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

        public Player(PlayerID playerID, GameObject paddle)
        {
            score = 0;
            this.playerID = playerID;
            this.paddle = paddle;

            paddle.GetComponent<PaddleController>().SetupPaddle(playerID);
        }
    }

    private Player[] player = new Player[2];
    private TableManager tableManager;
    private CameraController cameraController;

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

        player[0] = new Player(PlayerID.One, tableManager.paddle[0]);
        player[1] = new Player(PlayerID.Two, tableManager.paddle[1]);
    }
    
}
