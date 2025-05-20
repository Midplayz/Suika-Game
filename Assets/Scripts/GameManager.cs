using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] fruitPrefabs; 

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

        // Immediately sets it as settled 
        Fruit f = newFruit.GetComponent<Fruit>();
        f.hasDropped = false; 
    }
}
