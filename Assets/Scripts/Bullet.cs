using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 10f; // The speed of the bullet
    [SerializeField] private string targetTag = "Enemy"; // The tag of the target object

    private Transform target; // The target transform

    void Start()
    {
        // Find the closest object with the target tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = obj.transform;
            }
        }
    }

    void Update()
    {
        // If the target is null, destroy the bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the bullet towards the target at a constant speed
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Damage the enemy
            EnemyHealth health = other.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(CharacterPump.Instance.damage);
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}