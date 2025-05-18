using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public float knockbackForce = 12f;
    public Vector2 knockbackDirection = new Vector2(0.5f, 1f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (health != null)
            {
                health.TakeDamage(1);
            }

            if (rb != null)
            {
                Vector2 force = knockbackDirection.normalized * knockbackForce;
                rb.linearVelocity = Vector2.zero; // reset avant d'appliquer
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
