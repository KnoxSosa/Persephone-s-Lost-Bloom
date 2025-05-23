using UnityEngine;

public class ResolutionLock : MonoBehaviour
{
    [SerializeField] private int targetWidth = 1920;
    [SerializeField] private int targetHeight = 1080;
    [SerializeField] private bool fullScreen = false; // true pour plein écran, false pour fenêtré

    void Start()
    {
        Screen.SetResolution(targetWidth, targetHeight, fullScreen);
    }
}
