using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] fruitPrefabs; 
    public bool isGameOver = false; 

    [field: Header("References for Fruit")]
    public InGameUIManager inGameUIManager;
    public AudioManager audioManager;
    public Texture dottedLineTexture;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnMergedFruit(int type, Vector2 position)
    {
        int nextType = type + 1;

        if (nextType >= fruitPrefabs.Length)
        {
            Debug.Log("Max fruit reached!");
            return;
        }

        GameObject newFruit = Instantiate(fruitPrefabs[nextType], position, Quaternion.identity);

        Fruit f = newFruit.GetComponent<Fruit>();
        f.hasDropped = false;

        // Pushes nearby smaller fruits to prevent those random ahh collision issues lol
        Collider2D newFruitCol = newFruit.GetComponent<Collider2D>();
        if (newFruitCol != null)
        {
            float radius = Mathf.Max(newFruitCol.bounds.extents.x, newFruitCol.bounds.extents.y) + 0.1f; 
            float pushRadius = radius + 0.5f; 
            float pushForce = 5f;

            Collider2D[] nearby = Physics2D.OverlapCircleAll(position, pushRadius);
            foreach (var col in nearby)
            {
                Fruit otherFruit = col.GetComponent<Fruit>();
                if (otherFruit != null && otherFruit.fruitType < nextType && otherFruit != f)
                {
                    Rigidbody2D rb = otherFruit.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 pushDir = (rb.position - position).normalized;
                        rb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}
