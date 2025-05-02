using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;

    private Vector3 respawnPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetCheckpoint(Vector3 newPosition)
    {
        respawnPosition = newPosition;
    }

    public void Respawn(GameObject player)
    {
        Debug.Log("Respawn à : " + respawnPosition);  // Ajout du debug ici
        player.transform.position = respawnPosition;
    }
}
