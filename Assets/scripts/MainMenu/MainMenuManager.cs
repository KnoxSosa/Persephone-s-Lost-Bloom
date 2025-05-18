using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("first"); // remplace par le nom exact de ta scène (ex: "Level1")
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // Fonctionne uniquement dans le build
    }
}
