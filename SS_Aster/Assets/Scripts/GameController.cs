using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public int tilesToWin = 5;  // Number of tiles required to win
    private Dictionary<Transform, int> playerTileCount = new Dictionary<Transform, int>();

    // Called by the TileColorChange script when a player captures a tile
    public void TileCaptured(Transform player)
    {
        // If the player is already in the dictionary, increment their tile count
        if (playerTileCount.ContainsKey(player))
        {
            playerTileCount[player]++;
        }
        else
        {
            // If the player is not in the dictionary, add them and set their tile count to 1
            playerTileCount[player] = 1;
        }

        // Check if this player has won the game
        CheckForWin(player);
    }

    // Check if a player has won by capturing enough tiles
    private void CheckForWin(Transform player)
    {
        if (playerTileCount[player] >= tilesToWin)
        {
            // Declare the player as the winner
            Debug.Log(player.name + " has won the game!");

            // You can add additional win logic here, like displaying a UI message or ending the game
        }
    }
}
