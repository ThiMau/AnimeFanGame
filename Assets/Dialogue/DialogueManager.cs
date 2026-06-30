using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private string[] names;
    private string[] lines;

    private int currentLine;

    private bool isTalking;

    private void Awake()
    {
        Instance = this;

        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isTalking) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    public void StartDialogue(string[] speakerNames, string[] dialogueLines)
    {
        names = speakerNames;
        lines = dialogueLines;

        currentLine = 0;

        isTalking = true;

        dialoguePanel.SetActive(true);

        ShowLine();
    }

    void ShowLine()
    {
        nameText.text = names[currentLine];
        dialogueText.text = lines[currentLine];
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine >= lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void EndDialogue()
    {
        isTalking = false;

        dialoguePanel.SetActive(false);
    }

    public bool IsTalking()
    {
        return isTalking;
    }
}