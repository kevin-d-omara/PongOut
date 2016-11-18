using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);  // brick is destroyed
        }
    }

    private void OnDestroy()
    {
        // fancy graphics
    }
}
