using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    public Text[] playerScoreText = new Text[2];

    private GameObject gameOverUI;

    private void Awake()
    {
        gameOverUI = transform.Find("GameOver").gameObject;
        gameOverUI.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += ShowGameOverButtons;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ShowGameOverButtons;
    }

    public void ShowGameOverButtons()
    {
        gameOverUI.SetActive(true);
    }

    public void PlayGameAgain()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
