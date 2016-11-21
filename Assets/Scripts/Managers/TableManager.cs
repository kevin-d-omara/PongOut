using UnityEngine;
using System.Collections;

public enum SideLR { Left = -1, Right = 1 };
public enum SideTB { Top = 1, Bottom = -1 };

public class TableManager : MonoBehaviour
{
    // brick setup settings
    public int numRows = 2;   // depth (i.e. horizontal count of bricks)
    public int numCols = 8;   // vertical count of bricks
                                        // Unity unit spacing between:
    public float rowSpacing = 0.1f;     // row <> row
    public float colSpacing = 0.04f;    // column <> column
    public float goalSpacing = 0.1f;    // back row <> table edge (horizontal)
    public float wallSpacing = 0.1f;    // bricks   <> table edge (vertical)
    public float paddleSpacing = 0.2f;  // paddle <> front row

    // table & entity dimensions
    public float brickWidth = 0.2f;
    public float backgroundWidth = 10.24f;
    public float backgroundHeight = 6.2f;

    // fraction of the goal which will not be targetted during a ball spawn
    // i.e. top & bottom 1/6th
    public float inverseBallSpawnDeadzone = 6f;

    // prefabs
    public GameObject brickPrefab;
    public GameObject powerupBrickPrefab;
    public GameObject paddlePrefab;
    public GameObject ballPrefab;
    public GameObject backgroundPrefab;
    public GameObject singleEdgePrefab;

    private GameObject background;
    public GameObject GetBackground() { return background; }

    public GameObject[] paddle = new GameObject[2];

    public void SetupScene()
    {
        // setup background -> create + resize + center
        background = Instantiate(backgroundPrefab);
        SetObjectSize2D(background, backgroundWidth, backgroundHeight);
        background.transform.position = new Vector3(0f, 0f, 0f);

        // setup top/bottom border ---------------------------------------------
        float halfWidth  = backgroundWidth / 2f;
        float halfHeight = backgroundHeight / 2f;

        GameObject topWall = Instantiate(singleEdgePrefab);
        topWall.GetComponent<SingleEdgeController>().FitTo(halfWidth, halfHeight, SideTB.Top);
        GameObject botWall = Instantiate(singleEdgePrefab);
        botWall.GetComponent<SingleEdgeController>().FitTo(halfWidth, halfHeight, SideTB.Bottom);

        // setup bricks for both players
        paddle[0] = LayBrickSetAndPaddle(SideLR.Left);
        paddle[1] = LayBrickSetAndPaddle(SideLR.Right);
    }

    // Creates a set of bricks + paddle on the left or right side of the table.
    // A brick set consists of:
    //      - 'numRows' x 'numCols' bricks of type 'brickPrefab'
    //      - 1 brick of type 'powerupBrickPrefab' in the center of the back row
    private GameObject LayBrickSetAndPaddle(SideLR side)
    {
        float dir = (side == SideLR.Left) ? -1f : 1f;

        // determine brick height by fitting to the parameters:
        //      - # bricks, spacing between bricks, & spacing to table edge
        float edgeLoss = 2 * wallSpacing;
        float inBetweenLoss = (numCols - 1) * colSpacing;
        float availableHeight = backgroundHeight - edgeLoss - inBetweenLoss;
        float brickHeight = availableHeight / numCols;

        float placementX = (backgroundWidth / 2f - goalSpacing - brickWidth / 2f) * dir;

        // place brick set
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

        // place paddle -> create + resize + move
        GameObject paddle = Instantiate(paddlePrefab);
        PaddleController pCtrl = paddle.GetComponent<PaddleController>();
        SetObjectSize2D(paddle, pCtrl.width, pCtrl.height);

        Vector3 paddlePos = paddle.transform.position;
        float spacing = (paddleSpacing + pCtrl.width / 2f) * -1;
        paddlePos.x = placementX + (spacing + rowSpacing) * dir;

        paddle.transform.position = paddlePos;
        return paddle;
    }

    // Sets object to specific size in Unity units via adjusting its scale.
    // Note: this affects the BoxCollider2D too.
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

    // A new ball is created at the center of the table:
    //      - moving towards from last player to score
    //      - aimed randomly, but such that it will not reach the edges of the
    //        player's goal (see inverseBallSpawnDeadzone)
    public IEnumerator SpawnBall(PlayerID lastPlayerToScore)
    {
        GameObject ball = Instantiate(ballPrefab);

        // ball spawns towards away from last player to score
        float dir = lastPlayerToScore == PlayerID.One ? -1f : 1f;
        float speedX = ball.GetComponent<BallController>().speedX * dir;

        // randomize starting angle such that:
        //      - ball will never reach upper/lower fraction of goal specified
        //        by 'inverseBallSpawnDeadzone' (i.e. upper/lower 1/6th)
        // note: calculated using two similar triangles made from:
        //      - w = speedX, h = maxSpeedY
        //      - w = backgroundWidth/2, h = backgroundHeight/2 * (1 - deadzone)
        //      - solving for maxSpeedY
        float deadzone = 1f / inverseBallSpawnDeadzone;
        float inverseAspect = backgroundHeight / backgroundWidth;
        float maxSpeedY = (1f - deadzone) * inverseAspect * speedX;
        float speedY = Random.Range(-maxSpeedY, maxSpeedY);

        // pause before adding force; this gives players time to get oriented
        yield return new WaitForSeconds(1f);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(speedX, speedY));
    }
}
