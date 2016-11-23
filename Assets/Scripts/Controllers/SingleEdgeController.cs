﻿using UnityEngine;
using System.Collections;

public class SingleEdgeController : MonoBehaviour
{
    public delegate void EdgeHit();
    public static event EdgeHit OnEdgeHit;

    // Resizes EdgeCollider2D:
    //      - to overlay the top or bottom edge of the rectangle made by
    //        halfWidth and halfHeight
    //      - by adjusting (x,y) of its points
    public void FitTo(float halfWidth, float halfHeight, SideTB side)
    {
        float dir = (side == SideTB.Top) ? 1f : -1f;

        Vector2[] points = GetComponent<EdgeCollider2D>().points;
        points[0].x = -halfWidth;
        points[0].y = halfHeight * dir;
        points[1].x = halfWidth;
        points[1].y = halfHeight * dir;
        GetComponent<EdgeCollider2D>().points = points;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (OnEdgeHit != null)
            {
                OnEdgeHit();
            }
        }
    }
}
