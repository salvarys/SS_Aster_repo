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

    void Start()
    {
        // Initialize health
        currentHp = maxHp;
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

        Vector3 moveDir = new Vector3(x, 0, z) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDir, Space.World);
    }

    void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
        GameUI.instance.UpdateHealth(currentHp);
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add respawn or end game logic here
    }
}
