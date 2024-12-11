using UnityEngine;

public class TileColorChange : MonoBehaviour
{
    public Renderer tileRenderer;  // Renderer for the tile
    private Color originalColor;   // Store the original color of the tile

    void Start()
    {
        // Get the Renderer component of the tile
        if (tileRenderer == null)
        {
            tileRenderer = GetComponent<Renderer>();
        }

        // Save the original color of the tile
        originalColor = tileRenderer.material.color;
    }

    // This is called when something with a collider enters the tile's trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player or the enemy (by tag)
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // Get the object's color (assuming the object has a Renderer component)
            Renderer objectRenderer = other.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                // Change the tile color to the object's color
                tileRenderer.material.color = objectRenderer.material.color;
            }
        }
    }

    // This is called when something with a collider exits the tile's trigger
    void OnTriggerExit(Collider other)
    {
        // When the player or enemy leaves, change the tile color back to its original color
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            tileRenderer.material.color = originalColor;
        }
    }
}
