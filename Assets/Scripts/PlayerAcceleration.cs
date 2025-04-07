using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAcceleration : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float verticalSpeed = 5f;
    [SerializeField] GameStart gameStart;
    [SerializeField] Animator playerAnim;
    [SerializeField] AudioManager audioManager;

    public GameObject bulletInjectionPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 aimDirection = Vector2.right; 

    void Start()
    {
        audioManager.PlayStartSound();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        if(gameStart.gameStarted == true)
        {
            playerAnim.SetTrigger("gameIsStarted");
            float verticalInput = 0f;

            if (Input.GetKey(KeyCode.UpArrow)) verticalInput = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) verticalInput = -1f;

            rb.linearVelocity = new Vector2(forwardSpeed, verticalInput * verticalSpeed);
        }


    }

    void Update()
    {
        HandleAiming();

        if (Input.GetKeyDown(KeyCode.X))
        {
            playerAnim.SetTrigger("isAttacking");
            Shoot();
            audioManager.PlayShootSound();
        }
    }

    void HandleAiming()
    {
        aimDirection = Vector2.zero;

        //if (Input.GetKey(KeyCode.UpArrow)) aimDirection += Vector2.up;
        //if (Input.GetKey(KeyCode.DownArrow)) aimDirection += Vector2.down;
        //if (Input.GetKey(KeyCode.LeftArrow)) aimDirection += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow)) aimDirection += Vector2.right;

        if (aimDirection == Vector2.zero)
            aimDirection = Vector2.right; 
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletInjectionPrefab, firePoint.position, Quaternion.identity);
        BulletInjection bulletInjectionScript = bullet.GetComponent<BulletInjection>();
        if (bulletInjectionScript != null)
        {
            Vector2 extraVelocity = Vector2.right * forwardSpeed;
            bulletInjectionScript.SetDirection(aimDirection, extraVelocity);
        }
    }
}
