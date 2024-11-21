using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;  
    private Vector3 moveDirection;  
    public float directionChangeInterval = 2f; 

    private float timeSinceDirectionChange = 0f;

    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        // Move the enemy in the current direction
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Handle direction change over time
        timeSinceDirectionChange += Time.deltaTime;
        if (timeSinceDirectionChange >= directionChangeInterval)
        {
            ChangeDirection();
            timeSinceDirectionChange = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the enemy touches a tile, change its color to red
        if (collision.gameObject.CompareTag("Tile"))
        {
            Renderer tileRenderer = collision.gameObject.GetComponent<Renderer>();
            if (tileRenderer != null)
            {
                tileRenderer.material.color = Color.red;
            }

            // Optionally, bounce off the tile by reversing direction
            moveDirection = -moveDirection;
        }
    }

    private void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, 0f, randomZ).normalized;
    }
}
