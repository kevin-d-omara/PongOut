using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseController : MonoBehaviour
{
    public List<KeyCode> pauseKeys;

    private bool isPaused = false;

    private void OnEnable()
    {
        GameManager.OnGameOver += PauseGame;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= PauseGame;
    }

    private void Update()
    {
        if (isPaused)
        {
            foreach (KeyCode key in pauseKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    ResumeGame();
                }
            }
        }
        else
        {
            foreach (KeyCode key in pauseKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    PauseGame();
                }
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void OnDestroy()
    {
        ResumeGame();
    }
}
