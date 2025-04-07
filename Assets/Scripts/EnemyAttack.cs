using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float upwardForce = 5f;
    public float attackRate = 2f;
    public float attackRange = 10f;

    private Transform player;
    private float attackCooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackCooldown = 0f;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (attackCooldown <= 0f)
            {
                ThrowProjectileAtPlayer();
                attackCooldown = attackRate;
            }
        }

        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void ThrowProjectileAtPlayer()
    {
        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 target = player.position;
            Vector2 origin = throwPoint.position;

            float gravity = Physics2D.gravity.y * rb.gravityScale;
            float timeToHit = 0.8f;

            Vector2 velocity = CalculateTrajectory(origin, target, gravity, timeToHit);

            rb.linearVelocity = velocity;
        }
        Animator enemyAnim = GetComponent<Animator>();
        enemyAnim.Play("throw");
        Debug.Log("Enemy threw a projectile!");
    }

    Vector2 CalculateTrajectory(Vector2 origin, Vector2 target, float gravity, float time)
    {
        Vector2 distance = target - origin;
        float vx = distance.x / time;
        float vy = (distance.y - 0.5f * gravity * time * time) / time;
        return new Vector2(vx, vy);
    }
}
