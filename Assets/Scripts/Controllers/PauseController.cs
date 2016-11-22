using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseController : MonoBehaviour
{
    public List<KeyCode> pauseKeys;

    private bool isPaused = false;

    private void Update()
    {
        if (isPaused)
        {
            foreach (KeyCode key in pauseKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    Time.timeScale = 1f;    // resume game
                    isPaused = false;
                }
            }
        }
        else
        {
            foreach (KeyCode key in pauseKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    Time.timeScale = 0f;    // pause game
                    isPaused = true;
                }
            }
        }
    }
}
