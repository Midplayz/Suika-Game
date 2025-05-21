using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [field: Header("Spawning Values")]
    public GameObject[] fruitPrefabs;
    public Transform spawnLineTop;    
    public float slideRange = 2.5f;   

    private GameObject currentFruit;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewFruit();
    }

    void Update()
    {
        if (!GameManager.Instance.isGameOver && currentFruit != null)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            float clampedX = Mathf.Clamp(mousePos.x, -slideRange, slideRange);
            currentFruit.transform.position = new Vector3(clampedX, spawnLineTop.position.y, 0f);

            if (Input.GetMouseButtonDown(0)) // lmb will drop the fruit
            {
                DropFruit();
            }
        }
    }

    void SpawnNewFruit()
    {
        int rand = Random.Range(0, fruitPrefabs.Length - 2); // I subtracted two cuz big fruits, better not to spawn them
        currentFruit = Instantiate(fruitPrefabs[rand], spawnLineTop.position, Quaternion.identity);

        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        Collider2D col = currentFruit.GetComponent<Collider2D>();
        col.enabled = false;
    }

    void DropFruit()
    {
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;

        Collider2D col = currentFruit.GetComponent<Collider2D>();
        col.enabled = true;

        currentFruit = null;
        Invoke(nameof(SpawnNewFruit), 0.75f);
    }
}
