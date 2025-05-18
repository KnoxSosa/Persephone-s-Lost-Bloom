using UnityEngine;

public class JoystickButtonDebug : MonoBehaviour
{
    void Update()
    {
        for (int i = 0; i <= 15; i++)
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "JoystickButton" + i);
            if (Input.GetKeyDown(key))
            {
                Debug.Log("Bouton pressé : " + key);
            }
        }
    }
}
