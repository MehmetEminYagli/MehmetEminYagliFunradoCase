using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winnerPanel;
    
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        winnerPanel.SetActive(false);
        Time.timeScale = 1;

    }


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        winnerPanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void Winner()
    {
        gameOverPanel.SetActive(false);
        winnerPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void BtnTryAgain()
    {
        
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
    }
}
