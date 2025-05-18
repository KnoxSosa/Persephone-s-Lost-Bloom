using UnityEngine;
using System.Collections;

public class SpikeTrapController : MonoBehaviour
{
    public GameObject spikePrefab;   // prefab du pic à instancier
    public int spikeCount = 5;       // nombre de pics à générer
    public float delayBeforeActivation = 0.5f;

    public Vector2 areaSize = new Vector2(3f, 1f); // taille de la zone où les pics apparaissent (largeur x hauteur)
    public Vector2 areaCenterOffset = Vector2.zero; // décalage de la zone par rapport à SpikeTrapRoot

    public Transform spikeTrapRoot; // référence explicite au SpikeTrapRoot

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(SpawnSpikes());
        }
    }

    private IEnumerator SpawnSpikes()
    {
        yield return new WaitForSeconds(delayBeforeActivation);

        for (int i = 0; i < spikeCount; i++)
        {
            Vector2 randomPos2D = (Vector2)spikeTrapRoot.position + areaCenterOffset + new Vector2(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
            );

            Vector3 spawnPos = new Vector3(randomPos2D.x, randomPos2D.y, 0f); // z fixé à 0

            Instantiate(spikePrefab, spawnPos, Quaternion.identity);
        }
    }
}
