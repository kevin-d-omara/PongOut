using UnityEngine;
using System.Collections;

public enum SideLR { Left = -1, Right = 1 };
public enum SideTB { Top = 1, Bottom = -1 };

public class TableManager : MonoBehaviour
{
    public int numRows = 2;   // depth (i.e. horizontal count of bricks)
    public int numCols = 8;   // vertical count of bricks
                                        // Unity unit spacing between:
    public float rowSpacing = 0.1f;     // row <> row
    public float colSpacing = 0.04f;    // column <> column
    public float goalSpacing = 0.1f;    // back row <> table edge (horizontal)
    public float wallSpacing = 0.1f;    // bricks   <> table edge (vertical)
    public float paddleSpacing = 0.2f;  // paddle <> front row

    public float brickWidth = 0.2f;
    public float backgroundWidth = 10.24f;
    public float backgroundHeight = 6.2f;


    // prefabs
    public GameObject brickPrefab;
    public GameObject powerupBrickPrefab;
    public GameObject paddlePrefab;
    public GameObject backgroundPrefab;
    public GameObject singleEdgePrefab;

    private GameObject background;
    public GameObject GetBackground() { return background; }
    private GameObject topWall;
    private GameObject botWall;

    public void SetupScene()
    {
        // setup background -> create + resize + center
        background = Instantiate(backgroundPrefab);
        SetObjectSize2D(background, backgroundWidth, backgroundHeight);
        background.transform.position = new Vector3(0f, 0f, 0f);

        // setup top/bottom border ---------------------------------------------
        float halfWidth  = backgroundWidth / 2f;
        float halfHeight = backgroundHeight / 2f;

        topWall = Instantiate(singleEdgePrefab);
        topWall.GetComponent<SingleEdgeController>().FitTo(halfWidth, halfHeight, SideTB.Top);
        botWall = Instantiate(singleEdgePrefab);
        botWall.GetComponent<SingleEdgeController>().FitTo(halfWidth, halfHeight, SideTB.Bottom);

        // setup bricks for both players
        LayBrickSet(SideLR.Left);
        LayBrickSet(SideLR.Right);

        // place paddles
        // left
        // right
    }

    // Creates a set of bricks on the left or right side of the table.
    // A brick set consists of:
    //      - 'numRows' x 'numCols' bricks of type 'brickPrefab'
    //      - 1 brick of type 'powerupBrickPrefab' in the center of the back row
    private void LayBrickSet(SideLR side)
    {
        float dir = (side == SideLR.Left) ? -1f : 1f;

        // determine brick height by fitting to the parameters:
        //      - # bricks, spacing between bricks, & spacing to table edge
        float edgeLoss = 2 * wallSpacing;
        float inBetweenLoss = (numCols - 1) * colSpacing;
        float availableHeight = backgroundHeight - edgeLoss - inBetweenLoss;
        float brickHeight = availableHeight / numCols;

        float placementX = (backgroundWidth / 2f - goalSpacing - brickWidth / 2f) * dir;

        for (int i = 0; i < numRows; ++i)
        {
            float placementY = backgroundHeight / 2f - wallSpacing - brickHeight / 2f;

            for (int j = 0; j < numCols; ++j)
            {
                GameObject instance = Instantiate(brickPrefab);
                SetObjectSize2D(instance, brickWidth, brickHeight);                 // TODO: BoxCollider2D

                Vector3 position = instance.transform.position;
                position.x = placementX;
                position.y = placementY;
                instance.transform.position = position;

                placementY -= brickHeight + colSpacing;
            }

            placementX -= (brickWidth + rowSpacing) * dir;
        }
    }

    // Sets object to specific size in Unity units.
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
