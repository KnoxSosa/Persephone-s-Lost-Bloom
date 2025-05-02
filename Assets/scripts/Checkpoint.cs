using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)  // Utilisation de OnTriggerEnter2D pour les colliders 2D
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint touché !");
            RespawnManager.instance.SetCheckpoint(transform.position);
        }
    }
}
