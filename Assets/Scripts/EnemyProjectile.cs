using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthManager health = collision.GetComponent<PlayerHealthManager>();
            if (health != null)
            {
                health.TakeHit(); 
            }

            Destroy(gameObject); 
        }
    }
}
