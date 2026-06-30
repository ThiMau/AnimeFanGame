using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image icon;

    public TMP_Text nameText;

    public TMP_Text descText;

    public TMP_Text priceText;

    CardData card;

    public void Setup(CardData data)
    {
        card = data;

        icon.sprite = data.icon;

        nameText.text = data.cardName;

        descText.text = data.description;

        priceText.text = data.price.ToString();
    }

    public void Buy()
    {
        if (GameManager.Instance.SpendGold(card.price))
        {
            PlayerCardManager.Instance.AddCard(card);
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }
}