using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerHealth playerHealth; // Assure-toi de l'assigner dans l'inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(1);
        }
    }
}
