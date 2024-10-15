using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI playerInfoText;
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText;

    private PlayerController player;

    // instance
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    public void Initialize(PlayerController localPlayer)
    {
        player = localPlayer;
        UpdateHealthText();
        UpdatePlayerInfoText();
    }

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthText();
    }

    public void UpdatePlayerInfoText()
    {
        playerInfoText.text = "<b>Alive:</b> " + GameManager.instance.alivePlayers + "\n<b > Kills:</b> " + player.kills;
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
}
