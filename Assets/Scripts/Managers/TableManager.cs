using UnityEngine;
using System.Collections;

public class TableManager : MonoBehaviour
{
    public int numRows = 2;   // depth (i.e. horizontal count of bricks)
    public int numCols = 8;   // vertical count of bricks
    public float rowSpacing = 0.1f;     // units between rows
    public float colSpacing = 0.04f;    //               cols
    public float paddleSpacing = 0.2f;  //               paddle & front row

    public float backgroundWidth = 10.24f;
    public float backgroundHeight = 6.2f;

    // prefabs
    public GameObject brick;
    public GameObject powerupBrick;
    public GameObject paddle;
    public GameObject background;

    public void SetupScene()
    {
        // scale background
        


        // lay bricks
            // left
            // right
        
        // place paddles
            // left
            // right
    }
}
