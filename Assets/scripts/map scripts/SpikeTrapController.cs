using UnityEngine;
using System.Collections;

public class SpikeTrapController : MonoBehaviour
{
    public GameObject spikes;
    public float delayBeforeActivation = 0.5f;
    public float activeDuration = 1f;       // Durée pendant laquelle les pics sont visibles (reste en haut)
    public float cooldownDuration = 2f;

    private bool isOnCooldown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOnCooldown) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleSpikeTrap());
        }
    }

    private IEnumerator HandleSpikeTrap()
    {
        isOnCooldown = true;

        // Attente avant activation
        yield return new WaitForSeconds(delayBeforeActivation);

        // Active l'objet Spikes
        if (spikes != null)
        {
            spikes.SetActive(true);

            // Lance l'animation "Spike_Activate"
            Animator animator = spikes.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("Spike_Activate", -1, 0f);
            }
        }

        // Attente pendant que les pics restent en haut
        yield return new WaitForSeconds(activeDuration);

        // Désactive les pics
        if (spikes != null)
            spikes.SetActive(false);

        // Cooldown avant qu’on puisse réactiver le piège
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}
