using UnityEngine;
using System.Collections;

public class MainLoader : MonoBehaviour
{
    public GameManager gameManager; // GameManager prefab to instantiate

    void Awake()
    {
        Instantiate(gameManager);
    }
}
