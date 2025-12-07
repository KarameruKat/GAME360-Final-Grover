using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifetime = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;
    //private bool directionSet = false;

    [Header("Direction")]
    public Vector2 lastDirection = Vector2.down; // Default facing down

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;

        // Destroy bullet after lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Bullet hit enemy;
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(1);
                Destroy(gameObject); // Destroy bullet
            }
        }

         //Destroy bullet if it hits walls or boundaries
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    /*public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        directionSet = true;

        Debug.Log("Bullet direction set to: " + direction);

        // Immediately set velocity if Rigidbody2D exists
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }*/
}