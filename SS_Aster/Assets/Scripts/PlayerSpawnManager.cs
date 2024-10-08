using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;            // Prefab of the player object
    public Transform[] spawnPoints;            // Array of spawn points in the scene
    public Color[] playerColors;               // Array of colors for players

    void Start()
    {
        // Spawn players at the designated spawn points with unique colors
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Instantiate a player at each spawn point
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation);

            // Get the Renderer component of the player to change its color
            Renderer playerRenderer = newPlayer.GetComponent<Renderer>();

            // Assign a unique color from the playerColors array
            if (playerRenderer != null && i < playerColors.Length)
            {
                playerRenderer.material.color = playerColors[i];
            }

            // Optional: Set player names for easier identification
            newPlayer.name = "Player " + (i + 1);
        }
    }
}
