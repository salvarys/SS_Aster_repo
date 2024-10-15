using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;
    public int maxHp = 100;
    public int respawnTime = 5;  // Time to respawn after death

    [Header("Components")]
    public Rigidbody rig;
    public MeshRenderer mr;

    public int id;
    public Player photonPlayer;
    private int curAttackerId;

    public int curHp;
    public int kills;
    public bool dead;
    private bool flashingDamage;

    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine || dead)
            return;

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            TryJump();

        // Other input handling (e.g., capturing tiles) can go here
    }

    void Move()
    {
        // Get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate a direction relative to where we're facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;

        // Set that as our velocity
        rig.velocity = dir;
    }

    void TryJump()
    {
        // Create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);

        // Shoot the raycast
        if (Physics.Raycast(ray, 1.5f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    [PunRPC]
    public void Initialize(Player player)
    {
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;

        // Is this not our local player?
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            rig.isKinematic = true;
        }
        else
        {
            // GameUI.instance.Initialize(this);
        }
    }

    [PunRPC]
    public void TakeDamage(int attackerId, int damage)
    {
        if (dead)
            return;

        curHp -= damage;
        curAttackerId = attackerId;

        // Flash the player red
        photonView.RPC("DamageFlash", RpcTarget.Others);

        // Update the health bar UI
        // GameUI.instance.UpdateHealthBar();

        // Die if no health left
        if (curHp <= 0)
            photonView.RPC("Die", RpcTarget.All);
    }

    [PunRPC]
    void DamageFlash()
    {
        if (flashingDamage)
            return;

        StartCoroutine(DamageFlashCoroutine());

        IEnumerator DamageFlashCoroutine()
        {
            flashingDamage = true;
            Color defaultColor = mr.material.color;
            mr.material.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            mr.material.color = defaultColor;
            flashingDamage = false;
        }
    }

    [PunRPC]
    void Die()
    {
        curHp = 0;
        dead = true;

        GameManager.instance.alivePlayers--;

        // Host will check win condition
       // if (PhotonNetwork.IsMasterClient)
          //  GameManager.instance.CaptureTile(int playerId);

        // Is this our local player?
        if (photonView.IsMine)
        {
            if (curAttackerId != 0)
                GameManager.instance.GetPlayer(curAttackerId).photonView.RPC("AddKill", RpcTarget.All);

            // Disable the physics and hide the player
            rig.isKinematic = true;
            transform.position = new Vector3(0, -50, 0);

            // Respawn after a delay
            StartCoroutine(RespawnCoroutine());
        }
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnTime);

        photonView.RPC("Respawn", RpcTarget.All);
    }

    [PunRPC]
    void Respawn()
    {
        dead = false;
        curHp = maxHp;

        // Reset player position to a random spawn point
        Transform spawnPoint = GameManager.instance.spawnPoints[Random.Range(0, GameManager.instance.spawnPoints.Length)];
        transform.position = spawnPoint.position;
        rig.isKinematic = false;

        // Reset the player in the game
        GameManager.instance.alivePlayers++;
    }

    [PunRPC]
    public void AddKill()
    {
        kills++;

        // GameUI.instance.UpdatePlayerInfoText();
    }
}
