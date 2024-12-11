using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI playerInfoText;
    public TextMeshProUGUI tileCountText;

    private PlayerController player;

    public static GameUI instance;

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple GameUI instances detected. Destroying duplicate!");
            Destroy(this);
        }
    }

    public void Initialize(PlayerController localPlayer)
    {
        player = localPlayer;
        UpdateHealth(player.currentHp);
        UpdateKills(player.kills);
        UpdateTileCount(0); // Initialize tile count display
    }

    public void UpdateHealth(int health)
    {
        healthText.text = $"Health: {health}";
    }

    public void UpdateKills(int kills)
    {
        playerInfoText.text = $"Kills: {kills}";
    }

    public void UpdateTileCount(int tilesCaptured)
    {
        tileCountText.text = $"Tiles Captured: {tilesCaptured}";
    }
}
