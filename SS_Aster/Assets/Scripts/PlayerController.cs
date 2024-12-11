using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxHp = 100;
    public int kills = 0;

    public int currentHp { get; private set; }

    private Rigidbody rb;

    void Start()
    {
        // Initialize health
        currentHp = maxHp;

        // Cache Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("PlayerController: Rigidbody not found on the player!");
        }
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction and apply translation
        Vector3 moveDir = new Vector3(x, 0, z).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(moveDir, Space.World);

        // Optional: Rotate the player to face the movement direction
        if (moveDir.magnitude > 0)
        {
            transform.forward = moveDir;
        }
    }

    void Jump()
    {
        // Check if the player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        // Update health in the UI
        GameUI.instance?.UpdateHealth(currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");

    }

    public void AddKill()
    {
        kills++;
        GameUI.instance?.UpdateKills(kills);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle interactions with enemies or tiles
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with an enemy!");
            TakeDamage(10); // Example damage value
        }

        if (collision.gameObject.CompareTag("Tile"))
        {
            Debug.Log("Player stepped on a tile!");
            // Handle tile interaction logic (e.g., claiming the tile)
        }
    }
}
