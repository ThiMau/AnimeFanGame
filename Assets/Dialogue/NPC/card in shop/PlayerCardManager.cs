using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public static PlayerCardManager Instance;

    public List<CardData> cards = new();

    void Awake()
    {
        Instance = this;
    }

    public void AddCard(CardData card)
    {
        cards.Add(card);

        ApplyEffect(card);
    }

    void ApplyEffect(CardData card)
    {
        switch (card.type)
        {
            case CardType.Attack:

                PlayerStats.Instance.attack += card.value;

                break;

            case CardType.Health:

                PlayerStats.Instance.maxHP += card.value;

                break;

            case CardType.Defense:

                PlayerStats.Instance.defense += card.value;

                break;

            case CardType.Buff:

                break;

            case CardType.Special:

                break;
        }
    }
}