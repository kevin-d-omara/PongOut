using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour
{
    public delegate void BrickDestroyed();
    public static event BrickDestroyed OnBrickDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);  // brick is destroyed
            if (OnBrickDestroyed != null)
            {
                OnBrickDestroyed();
            }
        }
    }

    private void OnDestroy()
    {
        // fancy graphics
    }
}
