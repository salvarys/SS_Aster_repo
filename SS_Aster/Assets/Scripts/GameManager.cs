using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public Transform[] spawnPoints;  // Array of spawn points for both players and enemies
    public PlayerController playerPrefab;
    public EnemyBehavior enemyPrefab;  // Prefab for the enemy

    private PlayerController player;
    private List<EnemyBehavior> enemies = new List<EnemyBehavior>();

<<<<<<< HEAD
=======
    // Win condition: number of tiles required to win
    private const int tilesToWin = 7;

    public static GameManager instance;

    private Dictionary<int, int> playerTileCounts = new Dictionary<int, int>();  // Tracks player ID -> owned tiles count

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
>>>>>>> 9160cec0c70cb57cf38c7afdd9064e2b553b2c1b
    void Start()
    {
        // Spawn the player at a random spawn point
        Transform playerSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        // Initialize the UI with the player
        GameUI.instance.Initialize(player);

        // Spawn initial enemies
        SpawnEnemies(3);  // For example, spawn 3 enemies at the start
    }

    void SpawnEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Transform enemySpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            EnemyBehavior enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            enemies.Add(enemy);
        }
    }
}
