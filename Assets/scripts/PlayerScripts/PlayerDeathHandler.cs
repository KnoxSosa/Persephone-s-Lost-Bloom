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

        // Désactive le visuel et les collisions, mais pas tout l'objet
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        RespawnManager.instance.Respawn(gameObject);

        // Réactive tout
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}
