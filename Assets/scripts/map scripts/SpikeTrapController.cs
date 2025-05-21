using UnityEngine;
using System.Collections;

public class SpikeTrapController : MonoBehaviour
{
    public GameObject spikes;
    public float delayBeforeActivation = 0.5f;
    public float activeDuration = 1f;
    public float cooldownDuration = 2f;

    public AudioSource audioSource;
    public AudioClip activationSound;
    public AudioClip retractionSound;

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

        yield return new WaitForSeconds(delayBeforeActivation);

        // Active les pics
        if (spikes != null)
        {
            spikes.SetActive(true);

            // Animation
            Animator animator = spikes.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("Spike_Activate", -1, 0f);
            }

            // Son d’activation
            if (audioSource != null && activationSound != null)
            {
                audioSource.PlayOneShot(activationSound);
            }
        }

        yield return new WaitForSeconds(activeDuration);

        // Son de rétraction
        if (audioSource != null && retractionSound != null)
        {
            audioSource.PlayOneShot(retractionSound);
        }

        // Désactivation visuelle
        if (spikes != null)
        {
            spikes.SetActive(false);
        }

        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}
