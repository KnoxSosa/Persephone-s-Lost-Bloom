using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damageAmount = 1;

    // Knockback amélioré
    public float knockbackForceX = 20f; // horizontal + fort
    public float knockbackForceY = 20f; // vertical + fort

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);

                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockDirection = (other.transform.position.x < transform.position.x)
                        ? new Vector2(-knockbackForceX, knockbackForceY)
                        : new Vector2(knockbackForceX, knockbackForceY);

                    rb.linearVelocity = Vector2.zero; // Annule le mouvement en cours
                    rb.AddForce(knockDirection, ForceMode2D.Impulse);
                }
            }
        }
    }
}
