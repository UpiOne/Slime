using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Space(20)]
    [SerializeField] private float maxHealth = 100.0f; // Maximum health of the player
    [SerializeField] private float currentHealth; // Current health of the player (starts at maxHealth)

    [Header("UI Settings")]
    [Space(20)]
    [SerializeField] private Image healthBar; // Reference to the health bar Image component
    [SerializeField] private GameObject popupTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = CharacterPump.Instance.Hp; // Set the player's current health to the value from CharacterPump
        healthBar.fillAmount = 1.0f; // Set the health bar fill amount to 1.0f (full)
    }

    // Function to take damage and update the player's health
    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Subtract the amount of damage from the player's current health
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent health from going below 0
        healthBar.fillAmount = currentHealth / maxHealth; // Update the health bar fill amount
        ShowDamagePopup(amount.ToString()); // Show damage popup text
        if (currentHealth <= 0)
        {
            Die(); // If the player's health is zero or less, call the Die function
        }
    }

    // Function to handle the player's death
    private void Die()
    {
        // Do something to end the game, like show a game over screen or restart the level
        Debug.Log("Player has died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Function to show damage popup text
    void ShowDamagePopup(string text)
    {
        if (popupTextPrefab)
        {
            // Instantiate the popup text prefab at the player's position
            GameObject prefab = Instantiate(popupTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text; // Set the text of the popup text
            Destroy(prefab, 0.4f); // Destroy the popup text after 0.4 seconds
        }
    }
}