using UnityEngine;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    public float respawnDelay = 1.5f;
    public float deathAnimationDuration = 0.8f;

    private Animator animator;
    private SpriteRenderer sr;
    private Collider2D col;
    private Rigidbody2D rb;
    private PlayerMovement movement; // remplace par ton script de mouvement

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>(); // adapte le nom ici
    }

    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        Debug.Log("Début de DieRoutine");

        // 🔥 Lancer l'animation de mort
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // 🧊 Geler la position (arrêt net du mouvement)
        if (movement != null) movement.enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // stop mouvement
            rb.bodyType = RigidbodyType2D.Static; // figé totalement
        }

        // ⏳ Attente animation
        yield return new WaitForSeconds(deathAnimationDuration);

        // ❌ Désactivation temporaire
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        // 🔒 Verrouiller la caméra pendant la mort/respawn
        if (CameraController.Instance != null)
        {
            CameraController.Instance.cameraLocked = true;
        }

        // ⏳ Attente avant respawn
        yield return new WaitForSeconds(respawnDelay);

        // 🩺 Soins et réapparition
        GetComponent<PlayerHealth>().RestoreFullHealth();
        RespawnManager.instance.Respawn(gameObject);

        // ✅ Réactiver éléments
        if (sr != null) sr.enabled = true;
        if (col != null) col.enabled = true;
        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic; // restaurer physique
        if (movement != null) movement.enabled = true;

        // 🔓 Déverrouiller la caméra
        if (CameraController.Instance != null)
        {
            CameraController.Instance.cameraLocked = false;
        }

        // 🛡️ Invincibilité après respawn
        GetComponent<PlayerHealth>().ActivateInvincibility(false);
    }
}
