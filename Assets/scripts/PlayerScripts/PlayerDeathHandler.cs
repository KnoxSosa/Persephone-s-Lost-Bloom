using UnityEngine;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    public float respawnDelay = 1.5f;

    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        Debug.Log("Début de DieRoutine");

        // Désactivation du suivi de la caméra
        Camera.main.GetComponent<CameraFollow>().followEnabled = false;

        // Désactivation temporaire du visuel et des collisions du joueur
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // Restauration de la santé
        GetComponent<PlayerHealth>().RestoreFullHealth();

        // Respawn du joueur à la position du checkpoint
        RespawnManager.instance.Respawn(gameObject);

        // Réactivation du visuel et des collisions du joueur
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        // Réactivation du suivi caméra
        Camera.main.GetComponent<CameraFollow>().followEnabled = true;

        // Activation de l'invincibilité temporaire
        GetComponent<PlayerHealth>().ActivateInvincibility();
    }
}
