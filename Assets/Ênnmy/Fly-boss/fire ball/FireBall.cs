using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 10;
    public float lifeTime = 3f;

    private Vector2 direction;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (rb != null)
        {
            rb.velocity = direction * speed;
        }

        // Flip sprite
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Start()
    {
        Destroy(gameObject, lifeTime); // tự hủy
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}