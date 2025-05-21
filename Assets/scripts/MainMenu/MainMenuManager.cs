using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Transition")]
    public SceneTransitionManager transitionManager;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayGame()
    {
        StartCoroutine(PlaySoundAndStartTransition());
    }

    private System.Collections.IEnumerator PlaySoundAndStartTransition()
    {
        // Joue le son de clic
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            yield return new WaitForSeconds(0.2f); // court délai pour que le son démarre
        }

        // Lance la transition vers la scène
        if (transitionManager != null)
        {
            transitionManager.StartTransition();
        }
        else
        {
            Debug.LogWarning("TransitionManager n'est pas assigné !");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
