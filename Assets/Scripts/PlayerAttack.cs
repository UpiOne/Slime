using UnityEngine;

[System.Serializable] // Allows the variables to be shown in the Inspector
public class PlayerAttack : MonoBehaviour
{
    [Header("Bullet Settings")] // Group the following variables under a header in the Inspector
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float bulletArcHeight = 1f;

    [Header("Attack Settings")]
    public float fireRate = 0.5f;
    public LayerMask enemyLayer;
    public float attackRadius;
    public SlimeMover slimeMover;

    private float nextFireTime;

    private void Update()
    {
        // Check if it's time to fire
        if (Time.time > nextFireTime)
        {
            // Check for enemies in the attack radius
            Collider[] enemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);
            if (enemies.Length > 0)
            {
                // Disable character movement script if enemies are present
                slimeMover.enabled = false;

                // Shoot a bullet at the closest enemy
                GameObject closestEnemy = null;
                float closestDistance = Mathf.Infinity;
                foreach (Collider enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestEnemy = enemy.gameObject;
                        closestDistance = distance;
                    }
                }
                if (closestEnemy != null)
                {
                    ShootBullet(closestEnemy.transform.position);
                }
            }
            else
            {
                // Enable character movement script if no enemies are present
                slimeMover.enabled = true;
            }

            // Set the next fire time
            nextFireTime = Time.time + fireRate;
        }
    }

    private void ShootBullet(Vector3 targetPosition)
    {
        // Calculate bullet trajectory
        Vector3 direction = targetPosition - bulletSpawnPoint.position;
        float distance = direction.magnitude;
        direction.Normalize();

        float bulletTime = distance / bulletSpeed;
        float bulletHeight = bulletSpawnPoint.position.y + bulletArcHeight;

        // Calculate bullet velocity and add an upwards force to create an arc
        Vector3 bulletVelocity = direction * bulletSpeed;
        bulletVelocity.y += (bulletHeight - bulletSpawnPoint.position.y) / bulletTime;

        // Create bullet using object pool or Instantiate()
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bulletVelocity;

        // Destroy bullet after a set amount of time
        Destroy(bullet, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}