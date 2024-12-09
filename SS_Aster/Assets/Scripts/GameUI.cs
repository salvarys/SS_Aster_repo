using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI playerInfoText;

    private PlayerController player;

    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    public void Initialize(PlayerController localPlayer)
    {
        player = localPlayer;
        UpdateHealth(player.currentHp);
        UpdatePlayerInfo();
    }

    public void UpdateHealth(int health)
    {
        healthText.text = $"Health: {health}";
    }

    public void UpdatePlayerInfo()
    {
        playerInfoText.text = $"Kills: {player.kills}";
    }
}
