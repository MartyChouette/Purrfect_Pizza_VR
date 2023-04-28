using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI pointsText;
    private string _winMessage = "You Won!";
    private string _gameoverMessage = "Game Over";

    public void Setup(float score, bool goalReached)
    {
        this.gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
        if (goalReached)
        {
            messageText.text = _winMessage;
        }
        else
        {
            messageText.text = _gameoverMessage;
        }
    }

    public void RestartButton() 
    {   
        SceneManager.LoadScene("DemoScene");
    }

    public void ExitButton() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
