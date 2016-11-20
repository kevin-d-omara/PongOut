using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    public float startAngle = 12f;

    void Awake()
    {
        // testing
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(500f, startAngle));
        // end testing
    } 
}
