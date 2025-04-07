using UnityEngine;

public class BulletInjection : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float lifeTime = 2f;
    [SerializeField] AudioManager audioManager;

    private Vector2 direction;
    private Vector2 inheritedVelocity = Vector2.zero;

    public void SetDirection(Vector2 dir, Vector2 extraVelocity)
    {
        direction = dir.normalized;
        inheritedVelocity = extraVelocity;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        Vector2 totalVelocity = direction * speed + inheritedVelocity;
        transform.Translate(totalVelocity * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            Debug.Log("bullet hit enemtyy");
            Destroy(gameObject);

            Animator enemyAnim = collision.GetComponent<Animator>();
            enemyAnim.Play("attacked");
            audioManager.PlayHitSound();
        }


        if (collision.CompareTag("CamWall"))
        {
            Debug.Log("bullet hit cam wall");
            Destroy(gameObject);
        }
    }
}
