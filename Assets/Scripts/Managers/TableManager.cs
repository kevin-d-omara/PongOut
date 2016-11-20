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
    public GameObject brickPrefab;
    public GameObject powerupBrickPrefab;
    public GameObject paddlePrefab;
    public GameObject backgroundPrefab;

    private GameObject background;
    public GameObject GetBackground() { return background; }

    public void SetupScene()
    {
        background = Instantiate(backgroundPrefab);
        SetObjectSize2D(background, backgroundWidth, backgroundHeight);
        // center


        // lay bricks
            // left
            // right
        
        // place paddles
            // left
            // right
    }

    // Sets object to specific size in Unity Units.
    // Ex: SetObjectSize2D(brick, .2f, .76f) sets 'brick' to .2 units wide and
    //     .76 units tall, regardless of prior scale.
    private void SetObjectSize2D(GameObject obj, float xUnits, float yUnits)
    {
        Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;
        Bounds bounds = sprite.bounds;
        Vector3 size = bounds.size;

        float endScaleX = xUnits / size.x;
        float endScaleY = yUnits / size.y;

        obj.transform.localScale = new Vector3(endScaleX, endScaleY, 1f);
    }
}
