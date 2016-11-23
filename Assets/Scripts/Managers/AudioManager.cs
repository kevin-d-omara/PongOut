using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null; // singleton AudioManager

    private void Awake()
    {
        // enforce singleton pattern for AudioManager
        if (instance == null)
        {
            instance = this;            // first AudioManager instance becomes the singleton
        }
        else if (instance != this)
        {
            Destroy(gameObject);        // all other instances get destroyed
        }
        DontDestroyOnLoad(gameObject);  // preserve parent GameObject to preserve the singleton
    }
}
