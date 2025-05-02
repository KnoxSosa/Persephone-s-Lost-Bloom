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

        // Désactivation temporaire du visuel et des collisions
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // Respawn à la position du checkpoint
        RespawnManager.instance.Respawn(gameObject);

        // Réactivation du joueur
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        // Restauration des points de vie
        GetComponent<PlayerHealth>().RestoreFullHealth();
    }
}
