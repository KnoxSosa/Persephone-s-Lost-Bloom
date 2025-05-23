using UnityEngine;

public class DialogueTriggerPersephone : MonoBehaviour
{
    [TextArea(2, 4)]
    public string[] linesText; // Édite directement dans l’inspecteur

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player") && DialogueSystem.Instance != null && linesText.Length > 0)
        {
            triggered = true;

            // Convertit les string en DialogueLine[] automatiquement
            DialogueLine[] dialogueLines = new DialogueLine[linesText.Length];
            for (int i = 0; i < linesText.Length; i++)
            {
                dialogueLines[i] = new DialogueLine { text = linesText[i] };
            }

            DialogueSystem.Instance.ShowDialogue(dialogueLines);
        }
    }
}
