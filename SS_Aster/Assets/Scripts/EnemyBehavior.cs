using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public int health = 50;

    private GameManager gameManager;

    void Start()
    {
        // Attempt to find the player if not assigned
        if (target == null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("EnemyBehavior: Player target not found in the scene.");
            }
        }

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("EnemyBehavior: GameManager not found in the scene.");
        }
    }

    void Update()
    {
        if (target == null) return;

        // Move towards the player
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer > attackRange)
        {
            MoveTowardsTarget();
        }
        else
        {
            AttackPlayer();
        }

        // Check for death
        if (health <= 0)
        {
            Die();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        Debug.Log("Enemy is attacking the player!");
        // Add attack logic here (e.g., reduce player's health)
    }

    void Die()
    {
        Debug.Log("Enemy has died.");
        gameObject.SetActive(false); // Disable the enemy
        // Optionally respawn logic or destroy the object
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
