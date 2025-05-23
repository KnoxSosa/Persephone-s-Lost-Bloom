using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    public GameObject panelPersephone;
    public TMP_Text textPersephone;

    private DialogueLine[] dialogueLines;
    private int currentIndex = 0;
    private bool isDialogueActive = false;

    public bool IsDialogueActive => isDialogueActive;

    private PlayerMovement playerMovement;
    private Rigidbody2D playerRb;
    private Collider2D playerCol;

    // Timer pour empêcher le skip trop rapide
    private float inputCooldown = 0.3f;
    private float lastInputTime = 0f;

    public delegate void DialogueEvent();
    public event DialogueEvent OnDialogueStart;
    public event DialogueEvent OnDialogueEnd;

    void Awake()
    {
        Instance = this;
        panelPersephone.SetActive(false);
    }

    void Update()
    {
        if (!isDialogueActive) return;

        if (Time.time - lastInputTime < inputCooldown)
            return;  // Trop tôt, ignore l'input

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            lastInputTime = Time.time;  // reset timer

            currentIndex++;
            if (currentIndex < dialogueLines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void ShowDialogue(DialogueLine[] lines)
    {
        dialogueLines = lines;
        currentIndex = 0;
        isDialogueActive = true;
        OnDialogueStart?.Invoke();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            playerRb = player.GetComponent<Rigidbody2D>();
            playerCol = player.GetComponent<Collider2D>();

            if (playerMovement != null) playerMovement.enabled = false;
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.bodyType = RigidbodyType2D.Static;
            }
            if (playerCol != null) playerCol.enabled = false;
        }

        lastInputTime = Time.time;  // Pour pas que le joueur skip direct la 1ère ligne
        ShowLine();
    }

    private void ShowLine()
    {
        string text = dialogueLines[currentIndex].text;
        panelPersephone.SetActive(true);
        textPersephone.text = text;
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        panelPersephone.SetActive(false);
        OnDialogueEnd?.Invoke();

        if (playerMovement != null) playerMovement.enabled = true;
        if (playerRb != null) playerRb.bodyType = RigidbodyType2D.Dynamic;
        if (playerCol != null) playerCol.enabled = true;
    }
}
