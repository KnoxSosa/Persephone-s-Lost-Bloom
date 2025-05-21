using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject transitionCanvas;               // Canvas avec image + textes
    public Image fadeImage;                           // Image noire
    public TextMeshProUGUI mainText;                  // Citation
    public TextMeshProUGUI subText;                   // Auteur

    [Header("Menu UI")]
    public GameObject mainMenuCanvas;                 // Canvas des boutons à désactiver

    [Header("Audio")]
    public AudioSource musicSource;                   // Musique d'ambiance
    public float fadeDuration = 1.5f;                 // Durée du fade audio

    [Header("Scene")]
    public string sceneToLoad = "first";              // Nom de la scène à charger

    public void StartTransition()
    {
        // Désactive le menu pour bloquer les boutons
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);

        // Lance la transition
        StartCoroutine(PlayTransitionSequence());
    }

    private IEnumerator PlayTransitionSequence()
    {
        // Active le canvas de transition
        transitionCanvas.SetActive(true);

        // Configure les textes
        mainText.text = "“Because the world is so full of death and horror, I try again and again to console my heart and pick the flowers that grow in the midst of hell.”";
        subText.text = "-Hermann Hesse";
        mainText.alpha = 1f;
        subText.alpha = 1f;

        // Fade-in de l'écran noir
        yield return StartCoroutine(FadeImage(0f, 1f, 1f));

        // Pause pour laisser lire la citation
        yield return new WaitForSeconds(2f);

        // Fade audio
        if (musicSource != null)
            yield return StartCoroutine(FadeAudio(musicSource, fadeDuration, 0f));

        // Chargement de la scène suivante
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator FadeImage(float from, float to, float duration)
    {
        float timer = 0f;
        Color c = fadeImage.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / duration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeAudio(AudioSource source, float duration, float targetVolume)
    {
        float startVolume = source.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            yield return null;
        }
    }
}
