using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Renderer tileRenderer;      // Renderer for the tile
    private Color currentColor;        // Current color of the tile
    private GameController gameController; // Reference to the game controller
    private Transform lastEntityOnTile; // Last entity (player or enemy) that captured the tile

    void Start()
    {
        // Get the Renderer component
        tileRenderer = GetComponent<Renderer>();

        // Find the GameController in the scene
        gameController = FindObjectOfType<GameController>();

        // Initialize the current tile color
        currentColor = tileRenderer.material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the Player or Enemy
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Renderer entityRenderer = other.GetComponent<Renderer>();
            if (entityRenderer != null && other.transform != lastEntityOnTile)
            {
                // Change the tile's color to match the entity's color
                tileRenderer.material.color = entityRenderer.material.color;
                currentColor = entityRenderer.material.color;

                // Notify the game controller that this tile was captured
                gameController.TileCaptured(other.transform);

                // Update the last entity that captured this tile
                lastEntityOnTile = other.transform;
            }
        }
    }
}
