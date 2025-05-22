using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    public Vector3 cameraTargetPosition; // position fixe de la caméra quand le joueur entre ici

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.Instance.MoveToRoom(cameraTargetPosition);
        }
    }
}
