using UnityEngine;
using System;
using System.Collections;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public float width = 0.22f;
    public float height = 1f;
    public float edgeBuffer = 0.05f;

    [Serializable]
    public class SlopeIntercept
    {
        public float m;
        public float b;
        public SlopeIntercept(float m, float b) { this.m = m; this.b = b; }
    }
    public SlopeIntercept[] slopeIntercept = new SlopeIntercept[2];

    private PlayerID playerID;

    public void SetupPaddle(PlayerID playerID, Color color)
    {
        this.playerID = playerID;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveVertical = Input.GetAxis("Vertical" + (int)playerID);
        moveVertical *= speed * Time.deltaTime;
        gameObject.transform.Translate(0f, moveVertical, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Edge"))
        {
            SnapAwayFromEdge(collision);
        }
        else if (collision.CompareTag("Ball"))
        {
            ReboundFromPaddle(collision);
        }
    }

    // ball rebounds with:
    //      - velocity.x flipped
    //      - velocity.y defined by LinearRebound()'s linear partition function
    private void ReboundFromPaddle(Collider2D collision)
    {
        Vector2 velocity = collision.attachedRigidbody.velocity;

        velocity.x *= -1f;  // flip ball's x-direction of motion

        // determine ball's new y-direction of motion
        BallController ball = collision.gameObject.GetComponent<BallController>();
        Vector3 delta = collision.bounds.center - transform.position;
        velocity.y = LinearRebound(delta.y, ball.speedY);

        collision.attachedRigidbody.velocity = velocity;
    }

    // calculates y component of rebound force for ball
    // f(x) = m*x + b where {m,b} varies by partition of fourths
    private float LinearRebound(float x, float speedY)
    {
        float p4 = height / 4f;  // partition range into 1/4ths of paddle.height
        float m = 0;    // slope
        float b = 0;    // intercept

        // determine m & b based on inner or outer partition
        m = Mathf.Abs(x) > p4 ? slopeIntercept[0].m : slopeIntercept[1].m;
        b = Mathf.Abs(x) > p4 ? slopeIntercept[0].b : slopeIntercept[1].b;

        m *= speedY / p4;

        return m * x + b;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Edge"))
        {
            SnapAwayFromEdge(collision);
        }
    }

    // move paddle to be 'edgeBuffer' units away from the edge
    private void SnapAwayFromEdge(Collider2D collision)
    {
        // snap paddle away from edge
        Vector3 paddlePos = transform.position;
        Vector3 edgePos = collision.bounds.center;

        paddlePos.y = edgePos.y - (height / 2 + edgeBuffer) * Math.Sign(edgePos.y);

        transform.position = paddlePos;
    }
}
