using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameManager gameManager; // GameManager prefab to instantiate

    void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
