using UnityEngine;

public class TileManager : MonoBehaviour
{
    private bool isCaptured = false;
    private string capturedBy = "";

    private void OnTriggerEnter(Collider other)
    {
        if (isCaptured) return; // Prevent recapturing already captured tiles

        // Check if the collider belongs to a player
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            CaptureTile("Player");
            GameManager.instance.OnTileCaptured("Player"); // Notify GameManager of capture
            return;
        }

        // Check if the collider belongs to an enemy
        EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            CaptureTile("Enemy");
            GameManager.instance.OnTileCaptured("Enemy"); // Notify GameManager of capture
            return;
        }

        // If the collider doesn't have the required component, log it and ignore
        Debug.LogWarning($"Ignored collider: {other.name} (Tag: {other.tag})");
    }


    private void CaptureTile(string capturer)
    {
        isCaptured = true;
        capturedBy = capturer;
        Debug.Log($"Tile captured by: {capturedBy}");

        // Change the tile's appearance to indicate capture (e.g., change color)
        GetComponent<Renderer>().material.color = (capturer == "Player") ? Color.green : Color.red;

        // Notify the GameManager
        GameManager.instance.OnTileCaptured(capturedBy);
    }
}
