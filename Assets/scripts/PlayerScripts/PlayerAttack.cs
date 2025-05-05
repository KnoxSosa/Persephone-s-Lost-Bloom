using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject rootPrefab;
    public Transform rootSpawnPoint;
    public float cooldownTime = 10f;  // Temps du cooldown en secondes
    private float lastAttackTime = 0f;  // Temps du dernier lancement

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastAttackTime + cooldownTime)
        {
            LaunchRoot();
            lastAttackTime = Time.time;  // Met à jour le dernier temps d'attaque
        }
    }

    void LaunchRoot()
    {
        Instantiate(rootPrefab, rootSpawnPoint.position, rootSpawnPoint.rotation);
    }
}
