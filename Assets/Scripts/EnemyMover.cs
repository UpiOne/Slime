using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [Space(20)]
    [SerializeField] private float moveSpeed = 2.0f; // Speed at which the enemy moves towards the player
    [SerializeField] private float movementRadius = 10.0f; // Maximum distance the enemy can move from its starting position

    [Header("Attack Settings")]
    [Space(20)]
    [SerializeField] private float attackRange = 1.0f; // Distance at which the enemy can attack the player
    [SerializeField] private float attackCooldown = 1.0f; // Time between attacks
    [SerializeField] private int attackDamage = 10; // Amount of damage dealt per attack

    private bool isAttacking = false; // [Flag](poe://www.poe.com/_api/key_phrase?phrase=Flag&prompt=Tell%20me%20more%20about%20Flag.) to indicate whether the enemy is currently attacking
    private float attackCooldownTimer = 0.0f; // Timer to count down attack cooldown
    private GameObject player; // Reference to the player GameObject

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject by its tag
    }

    void Update()
    {
        // Check if player is within movement radius
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= movementRadius)
        {
            if (!isAttacking)
            {
                // Move towards player
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

                // Check if within attack range
                if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    isAttacking = true;
                    attackCooldownTimer = attackCooldown;

                    // Trigger attack animation
                    // ...
                }
            }
            else
            {
                // Count down attack cooldown timer
                attackCooldownTimer -= Time.deltaTime;

                if (attackCooldownTimer <= 0.0f)
                {
                    // Attack player and reset attack cooldown
                    player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                    attackCooldownTimer = attackCooldown;
                }
            }
        }
    }

    // Function to handle collision with player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
            attackCooldownTimer = attackCooldown;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, movementRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
