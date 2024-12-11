using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    private bool isJumping;
    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;

    // Health system
    public int maxHp = 100;
    private int currentHp;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        currentHp = maxHp; // Initialize health
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }

        // Handle interactions (e.g., capturing tiles) here if necessary
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            isJumping = false;
        }

        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
            _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center,
                                              capsuleBottom,
                                              distanceToGround,
                                              groundLayer,
                                              QueryTriggerInteraction.Ignore);

        return grounded;
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

        // Handle player death (e.g., disable controls, show respawn screen)
        Respawn();
    }

    void Respawn()
    {
        // Respawn logic
        currentHp = maxHp;
        transform.position = GetRandomSpawnPoint(); // Get a random spawn point
    }

    Vector3 GetRandomSpawnPoint()
    {
        Transform[] spawnPoints = GameManager.instance.spawnPoints;
        return spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}
