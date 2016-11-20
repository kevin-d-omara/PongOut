using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    void Awake()
    {
        // testing
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(500f, 10f));
        // end testing
    } 
}
