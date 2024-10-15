using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    [Header("Players")]
    public string playerPrefabLocation;
    public PlayerController[] players;
    public Transform[] spawnPoints;
    public int alivePlayers;
    public float postGameTime;
    public Color[] playerColors;
    private int playersInGame;

    // Maximum number of players
    private const int maxPlayers = 4;

    // Win condition: number of tiles required to win
    private const int tilesToWin = 7;

    // Instance for singleton pattern
    public static GameManager instance;

    // Tiles owned by each player
    private Dictionary<int, int> playerTileCounts = new Dictionary<int, int>();  // Tracks player ID -> owned tiles count

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        alivePlayers = players.Length;

        // Limit number of players
        if (players.Length > maxPlayers)
        {
            Debug.LogError("Too many players in the game! Max is 4.");
            return;
        }

        // Initialize player tile counts
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerTileCounts[player.ActorNumber] = 0;  // Start with 0 tiles for each player
        }

        // Ensure all players are in the game
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void ImInGame()
    {
        playersInGame++;
        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("SpawnPlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    void SpawnPlayer()
    {
        GameObject playerObject = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

        // Initialize the player for all other players
        PlayerController playerController = playerObject.GetComponent<PlayerController>();
        playerController.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);

        // Assign player's color based on their index
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        if (playerIndex < playerColors.Length)
        {
            Renderer playerRenderer = playerObject.GetComponent<Renderer>();
            if (playerRenderer != null)
            {
                playerRenderer.material.color = playerColors[playerIndex];
            }
        }
    }

    // Function to be called when a player captures a tile
    public void CaptureTile(int playerId)
    {
        if (!playerTileCounts.ContainsKey(playerId))
            return;

        playerTileCounts[playerId]++;

        // Check if the player has captured enough tiles to win
        if (playerTileCounts[playerId] >= tilesToWin)
        {
            photonView.RPC("WinGame", RpcTarget.All, playerId);
        }
    }

    [PunRPC]
    void WinGame(int winningPlayerId)
    {
        // Announce the winner
        Debug.Log("Player " + GetPlayer(winningPlayerId).photonPlayer.NickName + " has captured 7 tiles and won the game!");

        // Wait for a bit and then go back to the menu
        Invoke("GoBackToMenu", postGameTime);
    }

    void GoBackToMenu()
    {
        NetworkManager.instance.ChangeScene("Menu2");
    }

    public PlayerController GetPlayer(int playerId)
    {
        foreach (PlayerController player in players)
        {
            if (player != null && player.id == playerId)
                return player;
        }
        return null;
    }

    public PlayerController GetPlayer(GameObject playerObject)
    {
        foreach (PlayerController player in players)
        {
            if (player != null && player.gameObject == playerObject)
                return player;
        }
        return null;
    }
}
