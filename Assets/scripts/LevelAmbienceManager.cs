using UnityEngine;

public class LevelAmbienceManager : MonoBehaviour
{
    public AudioSource ambientSource;
    public AudioClip ambientClip;

    private void Start()
    {
        if (ambientSource != null && ambientClip != null)
        {
            ambientSource.clip = ambientClip;
            ambientSource.loop = true;
            ambientSource.Play();
        }
    }
}
