using UnityEngine;
using UnityEngine.UI; 

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;    // Maximum health
    private int currentHealth;     // Current health
    public Text healthText;        // Reference to the UI Text to display health

    void Start()
    {
        // Initialize the player's health to the maximum at the start
        currentHealth = maxHealth;

        // Update the UI to display the starting health
        UpdateHealthText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // Press space to take damage
        {
            TakeDamage(10);  // Reduce health by 10
        }
    }

    // Function to take damage
    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        currentHealth -= damage;

        // Ensure health does not go below 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Update the health text in the UI
        UpdateHealthText();

        // Check if health reaches zero
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Function to update the health display text
    private void UpdateHealthText()
    {
        // Update the text component with the current health value
        healthText.text = "Health: " + currentHealth.ToString();
    }

    // Function that triggers when health hits 0
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");

        // Optionally, you could destroy the object, trigger a respawn, or end the game
        // Destroy(gameObject);  // Example of destroying the GameObject
    }
}
