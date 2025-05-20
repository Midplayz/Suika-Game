using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null && fruit.hasDropped)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0; // Placeholder Game Over logic, you will need to make a proper one!

                enabled = false; // this will prevent repeated triggers so the script doesn't run continuously (Placeholder for now!!)

                // TODO: Add a Proper Game Over Logic
            }
        }
    }
}
