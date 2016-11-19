using UnityEngine;
using System.Collections;

public class TableManager : MonoBehaviour
{
    public int numRows = 2;   // depth (i.e. horizontal count of bricks)
    public int numCols = 8;   // vertical count of bricks
    public float rowSpacing = 0.1f;    // Unity Units between rows
    public float colSpacing = 0.04f;

    public GameObject brick;
    public GameObject powerupBrick;
    public GameObject paddle;

    private float paddleSpacing = 0.2f; // units between row & paddle

    public void SetupScene()
    {
        // lay bricks
            // left
            // right
        
        // place paddles
            // left
            // right
    }
}
