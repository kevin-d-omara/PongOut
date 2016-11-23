using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    public float speedX = 100f;
    public float speedY = 100f;

    private void OnEnable()
    {
        GameManager.OnBallPowerup += RespondToPowerup;
    }

    private void OnDisable()
    {
        GameManager.OnBallPowerup -= RespondToPowerup;
    }

    private void RespondToPowerup(string powerup)
    {
        if (powerup == "Rainbow")
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(Random.value, Random.value, Random.value, 1f);
        }
        else if (powerup == "Jink")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0f, Random.value * speedY), ForceMode2D.Impulse);
        }
        else if (powerup == "Accelerate")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.value * speedX, 0f));
        }
        else
        {
            // nothing
        }
    }
}
