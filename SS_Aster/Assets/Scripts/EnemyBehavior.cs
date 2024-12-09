using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public int maxHp = 50;
    public int damage = 10;
    public float respawnTime = 3f;
    private Vector3 moveDirection;  
    public float directionChangeInterval = 2f; 

    [Header("Movement")]
    public Transform[] patrolPoints; // Points for patrolling
    private int currentPatrolIndex = 0;

    [Header("Components")]
    public Rigidbody rig;
    public MeshRenderer meshRenderer;
    public PlayerController player;

    private int currentHp;
    private bool isDead = false;
    private bool isCapturing = false;

    void Start()
    {
        currentHp = maxHp;
        player = FindObjectOfType<PlayerController>();

    }

    void Update()
    {
        if (isDead) return;

        float v = Vector3.Distance(transform.position, player.transform.position);
        float distanceToPlayer = v;

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    // Patrol behavior
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        rig.velocity = new Vector3(direction.x * moveSpeed, rig.velocity.y, direction.z * moveSpeed);

        // Check if reached patrol point
        if (Vector3.Distance(transform.position, targetPoint.position) < 1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    // Chase the player
    void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rig.velocity = new Vector3(direction.x * moveSpeed, rig.velocity.y, direction.z * moveSpeed);
    }

    // Attack the player
    void AttackPlayer()
    {
        player.TakeDamage(damage);
        Debug.Log("Enemy attacked player!");
    }

    // Capture tile logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            if (!isCapturing)
            {
                StartCoroutine(CaptureTile(other.gameObject));
            }
        }
    }

    IEnumerator CaptureTile(GameObject tile)
    {
        isCapturing = true;

        // Simulate capturing time
        yield return new WaitForSeconds(2f);

        Debug.Log("Enemy captured a tile!");
        Destroy(tile); // Simulates capturing a tile
        isCapturing = false;
    }

    // Take damage from the player
    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy died!");
        rig.isKinematic = true;
        meshRenderer.enabled = false;

        // Start respawn coroutine
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);

        // Respawn logic
        Transform respawnPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
        transform.position = respawnPoint.position;

        currentHp = maxHp;
        isDead = false;
        rig.isKinematic = false;
        meshRenderer.enabled = true;

        Debug.Log("Enemy respawned!");
    }
}
