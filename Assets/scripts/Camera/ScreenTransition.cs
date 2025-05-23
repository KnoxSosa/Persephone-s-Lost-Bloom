using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTransition2D : MonoBehaviour
{
    public Image blackScreen;
    public float fadeDuration = 1f;
    public float waitAfterTeleport = 0.5f;
    public Transform teleportDestination;
    public GameObject player;

    public GameObject parallaxGroup1; // à désactiver
    public GameObject parallaxGroup2; // à activer

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        // Fade In (vers noir)
        yield return StartCoroutine(Fade(0, 1));

        // Téléportation
        player.transform.position = teleportDestination.position;

        // Changement de parallax
        if (parallaxGroup1 != null) parallaxGroup1.SetActive(false);
        if (parallaxGroup2 != null) parallaxGroup2.SetActive(true);

        // Attente après téléportation
        yield return new WaitForSeconds(waitAfterTeleport);

        // Fade Out (revenir à transparent)
        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = blackScreen.color;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            blackScreen.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        blackScreen.color = color;
    }
}
