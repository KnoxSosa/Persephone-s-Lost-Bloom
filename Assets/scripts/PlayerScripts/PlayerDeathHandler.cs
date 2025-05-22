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
    private PlayerMovement movement; // adapte le nom ici si nécessaire

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        Debug.Log("Début de DieRoutine");

        // 🔥 Animation de mort
        if (animator != null)
            animator.SetTrigger("Die");

        // 🧊 Geler le joueur
        if (movement != null) movement.enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // ⏳ Attente animation
        yield return new WaitForSeconds(deathAnimationDuration);

        // ❌ Masquer le joueur
        if (sr != null) sr.enabled = false;
        if (col != null) col.enabled = false;

        // 🔒 Verrouiller la caméra
        if (AdvancedCameraController.Instance != null)
            AdvancedCameraController.Instance.cameraLocked = true;

        // ⏳ Attente avant respawn
        yield return new WaitForSeconds(respawnDelay);

        // 🩺 Soigner et respawn
        GetComponent<PlayerHealth>().RestoreFullHealth();
        RespawnManager.instance.Respawn(gameObject);

        // ✅ Réactiver le joueur
        if (sr != null) sr.enabled = true;
        if (col != null) col.enabled = true;
        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;
        if (movement != null) movement.enabled = true;

        // 🔓 Déverrouiller la caméra
        if (AdvancedCameraController.Instance != null)
            AdvancedCameraController.Instance.cameraLocked = false;

        // 🛡️ Activer invincibilité temporaire
        GetComponent<PlayerHealth>().ActivateInvincibility(false);
    }
}
