using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    bool canTalk;

    void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            ShopUI.Instance.OpenShop();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
        }
    }
}