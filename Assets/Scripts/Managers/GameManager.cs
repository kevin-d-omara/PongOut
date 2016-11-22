using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public enum PlayerID { One = 1, Two = 2 };

/* GameManager handles
 *      - scene setup (via TableManager)
 *          - bricks
 *          - players
 *      - ball spawn/respawn
 *      - powerup countdown + delegation
 *      - score counter
 *      - gameover condition checker
 */
public class GameManager : MonoBehaviour
{
    public class Player
    {
        public PlayerID playerID;
        public GameObject paddle;
        public Text scoreText;
        public int Score
        {
            get { return score; }
            set { score = value; scoreText.text = String.Format("{0,2:D2}", value); }
        }
        private int score;

        public Player(PlayerID playerID, GameObject paddle, Color color)
        {
            this.playerID = playerID;
            this.paddle = paddle;

            UIController ui = FindObjectOfType<UIController>();
            this.scoreText = ui.playerScoreText[(int)playerID - 1];
            Score = 0;

            paddle.GetComponent<PaddleController>().SetupPaddle(playerID, color);
        }
    }
    public Color[] playerColor = new Color[2];

    private Player[] player = new Player[2];
    private TableManager tableManager;
    private CameraController cameraController;

    [SerializeField]
    private int pointsToWin = 10;
    private PlayerID lastPlayerToScore = PlayerID.One;
    private int ballCount = 0;

    private void OnEnable()
    {
        GoalController.OnGoalScored += OnGoalScored;
    }

    private void OnDisable()
    {
        GoalController.OnGoalScored -= OnGoalScored;
    }

    void Start()
    {
        tableManager = GetComponent<TableManager>();
        cameraController = GetComponent<CameraController>();

        InitGame();
    }

    void InitGame()
    {
        tableManager.SetupScene();
        cameraController.SetViewedObjectTo(tableManager.GetBackground());

        player[0] = new Player(PlayerID.One, tableManager.paddle[0], playerColor[0]);
        player[1] = new Player(PlayerID.Two, tableManager.paddle[1], playerColor[1]);

        StartCoroutine(tableManager.SpawnBall(lastPlayerToScore));
        ++ballCount;
    }
    
    private void OnGoalScored(GameObject ball)
    {
        lastPlayerToScore = ball.transform.position.x > 0 ? PlayerID.One : PlayerID.Two;

        Destroy(ball);
        --ballCount;
        player[(int)lastPlayerToScore - 1].Score += 1;

        if (player[0].Score >= pointsToWin || player[1].Score >= pointsToWin)
        {
            Debug.Log("Game Over!");
        }

        if (ballCount < 1)
        {
            StartCoroutine(tableManager.SpawnBall(lastPlayerToScore));
            ++ballCount;
        }
    }
}
