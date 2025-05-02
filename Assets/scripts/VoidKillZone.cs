using UnityEngine;

public class VoidKillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur est tomb� dans le vide !");
            other.GetComponent<PlayerDeathHandler>().Die();
        }
    }
}