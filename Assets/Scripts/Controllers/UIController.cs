using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    public Text[] playerScoreText = new Text[2];

    public void PlayGameAgain()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);


    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
