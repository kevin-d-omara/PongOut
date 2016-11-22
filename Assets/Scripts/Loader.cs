using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameManager gameManager; // GameManager prefab to instantiate

    void Awake()
    {
        Instantiate(gameManager);
    }
}
