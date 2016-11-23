using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

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
    public delegate void BallPowerup(string powerup);
    public static event BallPowerup OnBallPowerup;
    public List<string> ballPowerups;

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
    public float powerupFrequency = 5f;     // time between powerups
    public float powerupVariance = 2f;      // +- time delta on ^

    public delegate void GameOver();
    public static event GameOver OnGameOver;

    private Player[] player = new Player[2];
    private TableManager tableManager;
    private CameraController cameraController;

    [SerializeField]
    private int pointsToWin = 10;
    private PlayerID lastPlayerToScore = PlayerID.One;
    private int ballCount = 0;
    private float powerupTimer;

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
        ResetPowerupTimer();
    }

    private void Update()
    {
        powerupTimer -= Time.deltaTime;
        if (powerupTimer < 0f)
        {
            if (OnBallPowerup != null)
            {
                int index = ballPowerups.Count;
                OnBallPowerup(ballPowerups[Random.Range(0, index)]);
            }
            ResetPowerupTimer();
        }
    }

    private void OnGoalScored(GameObject ball)
    {
        lastPlayerToScore = ball.transform.position.x > 0 ? PlayerID.One : PlayerID.Two;

        Destroy(ball);
        --ballCount;
        player[(int)lastPlayerToScore - 1].Score += 1;

        if (player[0].Score >= pointsToWin || player[1].Score >= pointsToWin)
        {
            if (OnGameOver != null)
            {
                OnGameOver();
            }
            return;
        }

        if (ballCount < 1)
        {
            StartCoroutine(tableManager.SpawnBall(lastPlayerToScore));
            ++ballCount;
        }
    }

    private void ResetPowerupTimer()
    {
        powerupTimer = powerupFrequency + Random.Range(-powerupVariance, powerupVariance);
    }
}
