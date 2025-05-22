using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Vector3 targetPosition;
    private bool isMoving = false;
    public float transitionSpeed = 5f;

    // Booléen pour verrouiller la caméra (ex: pendant la mort/respawn)
    public bool cameraLocked = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving && !cameraLocked)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void MoveToRoom(Vector3 newRoomPosition)
    {
        if (cameraLocked) return; // empêche déplacement si verrouillé

        targetPosition = new Vector3(newRoomPosition.x, newRoomPosition.y, transform.position.z);
        isMoving = true;
    }
}
