using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public int tilesToWin = 5; 
    private Dictionary<Transform, int> playerTileCount = new Dictionary<Transform, int>();

    public void TileCaptured(Transform player)
    {
        if (playerTileCount.ContainsKey(player))
        {
            playerTileCount[player]++;
        }
        else
        {
            playerTileCount[player] = 1;
        }

        CheckForWin(player);
    }

    private void CheckForWin(Transform player)
    {
        if (playerTileCount[player] >= tilesToWin)
        {
            Debug.Log(player.name + " has won the game!");

        }
    }
}
