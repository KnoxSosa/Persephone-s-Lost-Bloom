using UnityEngine;

public class AdvancedCameraController : MonoBehaviour
{
    public static AdvancedCameraController Instance;

    [Header("Target & Mode")]
    public Transform player;
    private bool followPlayer = true;

    [Header("Follow Settings")]
    public Vector2 followOffset;
    public float followSmoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    [Header("Room Lock Settings")]
    private Vector3 targetPosition;
    private bool isMovingToRoom = false;
    public float roomTransitionSpeed = 5f;

    [Header("Camera Lock")]
    public bool cameraLocked = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (cameraLocked || player == null) return;

        if (followPlayer && !isMovingToRoom)
        {
            Vector3 target = new Vector3(player.position.x + followOffset.x, player.position.y + followOffset.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, followSmoothTime);
        }
        else if (isMovingToRoom)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, roomTransitionSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                isMovingToRoom = false;
            }
        }
    }

    /// <summary>
    /// Appelé par un CameraRoom pour déplacer la caméra vers une position fixe ou passer en mode follow
    /// </summary>
    public void MoveToRoom(Vector3 newRoomPosition, bool shouldFollow = false)
    {
        if (cameraLocked) return;

        followPlayer = shouldFollow;

        if (shouldFollow)
        {
            isMovingToRoom = false;
        }
        else
        {
            targetPosition = new Vector3(newRoomPosition.x, newRoomPosition.y, transform.position.z);
            isMovingToRoom = true;
        }
    }
}
