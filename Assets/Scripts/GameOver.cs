using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    public void Setup(int score) 
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
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
