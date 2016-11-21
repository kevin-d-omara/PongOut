using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour
{
    // publisher
    public delegate void GoalScored(GameObject ball);
    public static event GoalScored OnGoalScored;

    public void FitTo(float halfWidth, float height, SideLR side)
    {
        float destX = side == SideLR.Left ? -(halfWidth + 1f) : halfWidth + 1f;
        transform.position = new Vector3(destX, 0f, 0f);
        transform.localScale = new Vector3(1f, 2f * height, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (OnGoalScored != null)
            {
                OnGoalScored(collision.gameObject); // broadcast information
            }
        }
    }
}
