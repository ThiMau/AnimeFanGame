using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueTrigger : MonoBehaviour
{
    bool talked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER: " + other.name);

        if (talked) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER FOUND");

            talked = true;

            string[] names =
            {
                "ArchDemon",
                "Vegito",
                "ArchDemon",
                "Vegito"
            };

            string[] lines =
            {
                "Ngươi là kẻ nào?",
                "Well...đi lạc thôi...có ý kiến sao",
                "Có đó, chú mày làm phiền tới ta...và ta đơn giản là khử ngươi thôi",
                "Mày nói hơi nhiều so với 1 đứa có bảo hiểm nhân thọ đó"
            };

            DialogueManager.Instance.StartDialogue(names, lines);
        }
    }
}

//"Ngươi là kẻ nào?",
//"Well...đi lạc thôi...có ý kiến sao",
// "Có đó, chú mày làm phiền tới ta...và ta đơn giản là khử ngươi thôi",
// "được....bu vô đây"