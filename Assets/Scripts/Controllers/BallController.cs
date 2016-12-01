using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    public float speedX = 100f;
    public float speedY = 100f;

    // default velocity.x after single application of
    // rb.AddForce(new Vector2(speedX, speedY))
    private float defaultVelocityX;

    private void Awake()
    {
        defaultVelocityX = 0.02f * speedX;
    }

    private void OnEnable()
    {
        GameManager.OnActivateBallPowerup += RespondToPowerup;
    }

    private void OnDisable()
    {
        GameManager.OnActivateBallPowerup -= RespondToPowerup;
    }

    private void RespondToPowerup(BallPowerup powerup)
    {
        if (powerup == BallPowerup.Rainbow)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(Random.value, Random.value, Random.value, 1f);
        }
        else if (powerup == BallPowerup.Jink)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0f, Random.value * speedY), ForceMode2D.Impulse);
        }
        else if (powerup == BallPowerup.Boost)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float dir = Mathf.Sign(rb.velocity.x);
            rb.AddForce(new Vector2(Random.value * speedX * dir, 0f));
        }
        else if (powerup == BallPowerup.RetroBoost)
        {
            /* (save) -- AddForce() magnitude proportional to current velocity.x
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            float dir = -Mathf.Sign(rb.velocity.x);
            //float mult = Mathf.Ceil(Mathf.Abs(rb.velocity.x) / defaultVelocityX);
            float mult = Mathf.Abs(rb.velocity.x) / defaultVelocityX;
            float mod = mult * 2f * dir;

            rb.AddForce(new Vector2(Random.value * mod * speedX, 0f));
            */
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float dir = -Mathf.Sign(rb.velocity.x);
            rb.AddForce(new Vector2(Random.Range(0f, 1.5f) * speedX * dir, 0f));
        }
        else
        {
            // nothing
        }
    }
}
