using UnityEngine;
using System.Collections.Generic;

public class Fruit : MonoBehaviour
{
    [field: Header("Fruit Properties")]
    public int fruitType;
    public int merageValue;
    public bool hasDropped = false;

    private Rigidbody2D rb;
    private float settleCheckTime = 0.5f;
    private float stillTimer = 0f;
    private float velocityThreshold = 0.05f;

    private List<Fruit> overlappingSameTypeFruits = new();

    private UiGameManager uigameManager;
    private AudioManager audioManager;
    private bool isDropSFx = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        uigameManager = GameObject.Find("UIGameManager").GetComponent<UiGameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        isDropSFx = false; // Reset the drop SFX flag at the start
    }

    private void Update()
    {
        if (!hasDropped && rb.bodyType == RigidbodyType2D.Dynamic && IsTouchingSomething())
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
                stillTimer = 0f;
            }
        }

        TryMerge(); // I put this in Update since I want it to check and try just in case it touches another fruit due to random movemement.
    }

    private bool IsTouchingSomething() //Well, this checks if the fruit is touching something
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        Collider2D[] results = new Collider2D[1];
        return GetComponent<Collider2D>().Overlap(filter, results) > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Fruit other = collision.gameObject.GetComponent<Fruit>();
        if (other != null && other.fruitType == fruitType && !overlappingSameTypeFruits.Contains(other))
        {
            overlappingSameTypeFruits.Add(other);
        }
        // Play drop SFX immediately on first touch
        if (!hasDropped && isDropSFx == false)
        {
            audioManager.PlayDropSfx();
            isDropSFx = true; // Mark as dropped to avoid multiple SFX
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Fruit other = collision.gameObject.GetComponent<Fruit>();
        if (other != null && overlappingSameTypeFruits.Contains(other))
        {
            overlappingSameTypeFruits.Remove(other);
        }
    }

    private void TryMerge()
    {
        if (!hasDropped) return;

        for (int i = overlappingSameTypeFruits.Count - 1; i >= 0; i--)
        {
            Fruit other = overlappingSameTypeFruits[i];
            if (other == null || !other.hasDropped) continue;

            if (fruitType == other.fruitType)
            {
                // Avoids Double merging cuz that's bad
                if (this.GetInstanceID() < other.GetInstanceID())
                {
                    MergeWith(other);
                }
                overlappingSameTypeFruits.Remove(other);
                break;
            }
        }
    }

    private void MergeWith(Fruit other)
    {
        Vector2 mergePosition = (transform.position + other.transform.position) / 2f;
        int nextType = fruitType + 1;

        if (nextType < GameManager.Instance.fruitPrefabs.Length)
        {
            GameManager.Instance.SpawnMergedFruit(fruitType, mergePosition);

            // updating ui 
            uigameManager.addScore(merageValue);
        }
        else
        {
            Debug.Log("Max level fruit merged! Destroying both.");
        }

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
