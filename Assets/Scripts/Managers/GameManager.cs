using UnityEngine;
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
    public static GameManager instance = null;  // singlton GameManager
    public class Player
    {
        public int score;
        public PlayerID playerID;
        public GameObject paddle;

        public Player(PlayerID playerID, GameObject paddle, Color color)
        {
            score = 0;
            this.playerID = playerID;
            this.paddle = paddle;

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

    void Awake()
    {
        // enforce singleton pattern for GameManager
        if (instance == null)
        {
            instance = this;            // first GameManager instance becomes the singleton
        }
        else if (instance != this)
        {
            Destroy(gameObject);        // all other instances get destroyed
        }
        DontDestroyOnLoad(gameObject);  // preserve parent GameObject to preserve the singleton
    }

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
        player[(int)lastPlayerToScore - 1].score += 1;

        if (player[0].score >= pointsToWin || player[1].score >= pointsToWin)
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
