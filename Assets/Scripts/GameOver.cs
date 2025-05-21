using TMPro;
using UnityEngine;


public class GameOverTrigger : MonoBehaviour
{
    [field: Header("Game Over Stuff")]
    [field: SerializeField] private TextMeshProUGUI finalScoreText;
    [field: SerializeField] private InGameUIManager inGameUIManager;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null && fruit.hasDropped)
            {
                enabled = false;

                GameManager.Instance.isGameOver = true;
                finalScoreText.text = "Final Score: " + inGameUIManager.ReturnScore().ToString();
            }
        }
    }
}
