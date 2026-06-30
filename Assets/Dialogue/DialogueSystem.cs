using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject choicePanel;

    public Button fightButton;
    public Button leaveButton;
    public Button questionButton;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);

        nameText.text = "???";
        dialogueText.text = "Ngươi tới đây để chiến đấu… hay quỳ gối?";

        choicePanel.SetActive(true);

        fightButton.onClick.AddListener(FightChoice);
        leaveButton.onClick.AddListener(LeaveChoice);
        questionButton.onClick.AddListener(QuestionChoice);
    }

    void FightChoice()
    {
        dialogueText.text = "Tốt… Ta thích ánh mắt đó.";

        choicePanel.SetActive(false);

        // Trigger Boss Fight
    }

    void LeaveChoice()
    {
        dialogueText.text = "Hèn nhát. Biến khỏi tầm mắt ta.";

        choicePanel.SetActive(false);

        Invoke(nameof(CloseDialogue), 2f);
    }

    void QuestionChoice()
    {
        dialogueText.text = "Kẻ đã xóa sổ thế giới này…";

        choicePanel.SetActive(false);
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}