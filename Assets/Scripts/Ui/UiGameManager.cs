using TMPro;
using UnityEngine;
using MaskTransitions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiGameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int TotalScore ;
    public bool isGameOver = false;
    public GameObject gameoverUIParent;
    public Button restartButton;
    private bool isRestarting = false;

    private void Start()
    {
        TotalScore = 0;
        scoreText.text = "Score: " + TotalScore.ToString();
        isGameOver = false;
        gameoverUIParent.SetActive(false);
    
    }
    private void Update()
    {
        EnablingUI();
    
    }
    public void addScore(int score)
    {
        TotalScore = TotalScore + score;
        scoreText.text = "Score: " + TotalScore.ToString();
    }
    public  void RestartButtonFunction()
    {
        isRestarting = true;
        if (restartButton != null)
        {
            restartButton.interactable = false; // Prevent multiple clicks
        }
        TransitionManager.Instance.LoadLevel("MainGame");
       
    }
    public void MainMenuFunction()
    {
       TransitionManager.Instance.LoadLevel("MainMenu");
    }
    private void EnablingUI()
    {
        if(isGameOver == true)
        {
            gameoverUIParent.SetActive(true);
           if(isRestarting == true)
           {
                Time.timeScale = 1f; // Reset time scale to normal
           }
        }
    }
    
}
