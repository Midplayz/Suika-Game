using TMPro;
using UnityEngine;

public class UiGameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int TotalScore ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    public void addScore(int score)
    {
        TotalScore = TotalScore + score;
        scoreText.text = "Score: " + TotalScore.ToString();
    }
}
