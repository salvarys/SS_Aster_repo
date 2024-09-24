using UnityEngine;
using UnityEngine.UI; 

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;  
    private int currentHealth;
    public Text healthText;        

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            TakeDamage(10); 
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthText();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    private void Die()
    {
        Debug.Log( "Player has died!");
    }
}
