using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    public bool followEnabled = true;  // Booléen pour activer ou désactiver le suivi

    private void Update()
    {
        if (followEnabled && target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
