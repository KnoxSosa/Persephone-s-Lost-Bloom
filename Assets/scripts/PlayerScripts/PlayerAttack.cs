using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject rootPrefab;
    public Transform rootSpawnPoint;
    public float cooldownTime = 10f;
    private float lastAttackTime = 0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastAttackTime + cooldownTime)
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
            Vector2 dir = spriteRenderer.flipX ? Vector2.left : Vector2.right;
            rootAttack.direction = dir;
            Debug.Log("🎯 Direction envoyée au projectile : " + dir);
        }
    }
}
