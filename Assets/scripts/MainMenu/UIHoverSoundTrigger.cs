using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverSoundTrigger : MonoBehaviour, ISelectHandler
{
    public AudioClip hoverSound;

    public void OnSelect(BaseEventData eventData)
    {
        if (hoverSound != null)
        {
            AudioSource.PlayClipAtPoint(hoverSound, Vector3.zero); // joue en 2D
        }
    }
}
