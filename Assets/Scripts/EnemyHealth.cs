using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Space(20)]
    [SerializeField] private float maxHealth = 100.0f; // Maximum health of the enemy
    [SerializeField] private float currentHealth; // Current health of the enemy (starts at maxHealth)
    [SerializeField] private Image healthBar; // Reference to the health bar Image component
    [Header("Currency Settings")]
    [Space(20)]
    [SerializeField] private int currencyValue;
    [SerializeField] private GameObject popupTextPrefab;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private CoinsManager currency;

    // Start is called before the first frame update
    void Start()
    {
        currency = FindObjectOfType<CoinsManager>();
        currentHealth = maxHealth; // Set the enemy's current health to the maximum at the start of the game
        healthBar.fillAmount = 1.0f; // Set the health bar fill amount to 1.0f (full)
    }

    // Function to take damage and update the enemy's health
    public void TakeDamage(float amount)
    {

        currentHealth -= amount; // Subtract the amount of damage from the enemy'ss current health
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent health from going below 0
        healthBar.fillAmount = currentHealth / maxHealth; // Update the health bar fill amount
        ShowDPS(amount.ToString());
        if (currentHealth <= 0)
        {
            Die(); // If the enemy's health is zero or less, call the Die function
        }
    }

    // Function to handle the enemy's death
    private void Die()
    {
        var random = Random.Range(5, 20);
        // Do something to end the game, like show a game over screen or restart the level
        Debug.Log("Enemy has died!");
        Destroy(gameObject);
        Vector3 newPosition = new Vector3(transform.position.x, 0.4f, transform.position.z);
        Destroy(Instantiate(Prefab,newPosition, Quaternion.Euler(0f, 90f, 0f)),.3f);
        currency.AddCoins(gameObject.transform.position,random);
    }

    void ShowDPS(string text)
    {
        if (popupTextPrefab)
        {
            GameObject prefab = Instantiate(popupTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
            Destroy(prefab, 0.4f);
        }
    }
}