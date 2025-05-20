using UnityEngine;


public class GameOverTrigger : MonoBehaviour
{
    private UiGameManager uigameManager;
    private void Awake()
    {
        uigameManager = GameObject.Find("UIGameManager").GetComponent<UiGameManager>();
    
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null && fruit.hasDropped)
            {
                Time.timeScale = 0;
                Debug.Log("Game Over!");
                enabled = false; // this will prevent repeated triggers so the script doesn't run continuously (Placeholder for now!!)
                uigameManager.isGameOver = true; // Set the game over state in the UI manager
               
            }
        }
    }

}
