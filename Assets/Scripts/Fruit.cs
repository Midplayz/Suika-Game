using UnityEngine;

public class Fruit : MonoBehaviour
{
    [field: Header("Fruit Properties")]
    public int fruitType;
    public bool hasDropped = false;

    private Rigidbody2D rb;
    private float settleCheckTime = 0.5f;
    private float stillTimer = 0f;
    private float velocityThreshold = 0.05f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hasDropped) return;

        // Checks if it's colliding and has stopped moving
        if (rb.bodyType == RigidbodyType2D.Dynamic && IsTouchingSomething())
        {
            if (rb.linearVelocity.magnitude < velocityThreshold)
            {
                stillTimer += Time.deltaTime;

                if (stillTimer >= settleCheckTime)
                {
                    hasDropped = true;
                }
            }
            else
            {
                stillTimer = 0f; // reset timer if moving again
            }
        }
    }

    private bool IsTouchingSomething()
    {
        // Check if the fruit is touching anything like the container or other fruits or something
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        Collider2D[] results = new Collider2D[1];

        return GetComponent<Collider2D>().Overlap(filter, results) > 0;
    }
}
