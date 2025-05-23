using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject rootPrefab;
    public Transform rootSpawnPoint;
    public float cooldownTime = 10f;
    private float lastAttackTime = 0f;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton6))
            && Time.time >= lastAttackTime + cooldownTime)
        {
            LaunchRoot();
            lastAttackTime = Time.time;
        }
    }

    void LaunchRoot()
    {
        GameObject root = Instantiate(rootPrefab, rootSpawnPoint.position, rootSpawnPoint.rotation);

        RootAttack rootAttack = root.GetComponent<RootAttack>();
        if (rootAttack != null)
        {
            // 👉 utilise la direction du scale (comme dans ton script de mouvement)
            float scaleX = transform.localScale.x;
            Vector2 dir = scaleX > 0 ? Vector2.right : Vector2.left;
            rootAttack.direction = dir;

            Debug.Log("🎯 Direction envoyée au projectile : " + dir);
        }
    }
}
