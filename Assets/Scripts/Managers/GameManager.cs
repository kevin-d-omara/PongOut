using UnityEngine;
using System.Collections;

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
    
    private TableManager tableManager;

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

        tableManager = GetComponent<TableManager>();
        InitGame();
    }

    void InitGame()
    {
        tableManager.SetupScene();
    }
    
}
