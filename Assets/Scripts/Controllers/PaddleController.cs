using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
    public float speed;

    private PlayerID playerID;

    public void SetupPaddle(PlayerID playerID)
    {
        this.playerID = playerID;
        // change color
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveVertical = Input.GetAxis("Vertical" + (int)playerID);
        moveVertical *= speed * Time.deltaTime;
        gameObject.transform.Translate(0f, moveVertical, 0f);
    } 
}
