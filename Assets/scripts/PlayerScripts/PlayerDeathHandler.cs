using UnityEngine;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    public float respawnDelay = 1.5f;
    public float deathAnimationDuration = 0f; // Durée de l'animation de mort

    private Animator animator;
    private SpriteRenderer sr;
    private Collider2D col;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        Debug.Log("Début de DieRoutine");

        // 🔥 Déclencher l'animation de mort
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // ⏳ Attendre que l'animation se termine
        yield return new WaitForSeconds(deathAnimationDuration);

        // Désactivation du suivi de la caméra
        Camera.main.GetComponent<CameraFollow>().followEnabled = false;

        // Désactivation temporaire du visuel et des collisions
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        // ⏳ Attente avant respawn
        yield return new WaitForSeconds(respawnDelay);

        // Restauration de la santé
        GetComponent<PlayerHealth>().RestoreFullHealth();

        // Respawn à la position du checkpoint
        RespawnManager.instance.Respawn(gameObject);

        // Réactivation du visuel et des collisions
        if (sr != null) sr.enabled = true;
        if (col != null) col.enabled = true;

        // Réactivation du suivi caméra
        Camera.main.GetComponent<CameraFollow>().followEnabled = true;

        // Activation de l'invincibilité temporaire sans clignotement rouge
        GetComponent<PlayerHealth>().ActivateInvincibility(false);
    }
}
