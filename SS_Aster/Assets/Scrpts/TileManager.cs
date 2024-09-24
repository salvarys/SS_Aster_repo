using UnityEngine;

public class TileManager: MonoBehaviour
{
    public Renderer tileRenderer;       // Renderer for the tile
    private Color currentColor;         // The current color of the tile
    private GameController gameController; // Reference to the game controller

    private Transform lastPlayerOnTile;  // Keep track of the last player that captured the tile

    void Start()
    {
        // Get the Renderer component of the tile
        tileRenderer = GetComponent<Renderer>();

        // Find the GameController in the scene
        gameController = FindObjectOfType<GameController>();

        // Initialize the tile with its original color
        currentColor = tileRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is a player (by tag)
        if (other.CompareTag("Player"))
        {
            // Get the player's color (assuming the player has a Renderer component)
            Renderer playerRenderer = other.GetComponent<Renderer>();
            if (playerRenderer != null && other.transform != lastPlayerOnTile)
            {
                // Change the tile color to the player's color
                tileRenderer.material.color = playerRenderer.material.color;
                currentColor = playerRenderer.material.color;

                // Notify the GameController that the player has captured the tile
                gameController.TileCaptured(other.transform);

                // Remember the last player that captured the tile
                lastPlayerOnTile = other.transform;
            }
        }
    }
}
