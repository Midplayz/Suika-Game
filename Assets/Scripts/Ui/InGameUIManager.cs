using TMPro;
using UnityEngine;
using MaskTransitions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InGameUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score ;
    private int totalScore ;
    public GameObject gameOverUIParent;
    public Button restartButton;
    private bool isRestarting = false;

    private void Start()
    {
        totalScore = 0;
        scoreText.text = "Score: " + totalScore.ToString();
        GameManager.Instance.isGameOver = false;
        gameOverUIParent.SetActive(false);
    }
    private void Update()
    {
        EnablingUI();
    }
    public void addScore(int Score)
    {
        this.score = Score;
        totalScore = totalScore + Score;
        scoreText.text = "Score: " + totalScore.ToString();
    }
    public void RestartButtonFunction()
    {
        isRestarting = true;
        if (restartButton != null)
        {
            restartButton.interactable = false;
        }
        TransitionManager.Instance.LoadLevel("MainGame");
    }
    public void MainMenuFunction()
    {
       TransitionManager.Instance.LoadLevel("MainMenu");
    }

    public int ReturnScore()
    {
        return totalScore;
    }

    private void EnablingUI()
    {
        if(GameManager.Instance.isGameOver == true)
        {
            gameOverUIParent.SetActive(true);
           if(isRestarting == true)
           {
                Time.timeScale = 1f; 
           }
        }
    }
    
}
