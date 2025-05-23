using UnityEngine;

public class CameraRoom : MonoBehaviour
{
    public Vector3 cameraTargetPosition;
    public bool enableFollowMode = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AdvancedCameraController.Instance.MoveToRoom(cameraTargetPosition, enableFollowMode);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !enableFollowMode)
        {
            AdvancedCameraController.Instance.MoveToRoom(Vector3.zero, true);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Choix de couleur selon le mode
        Gizmos.color = enableFollowMode ? Color.green : Color.red;

        // Taille du cadre = taille caméra 16:9 (ajuste si nécessaire)
        Vector3 gizmoSize = new Vector3(16f, 9f, 0f);
        Gizmos.DrawWireCube(cameraTargetPosition, gizmoSize);
    }
#endif
}
