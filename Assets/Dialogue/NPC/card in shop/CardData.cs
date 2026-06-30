using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Shop/Card")]
public class CardData : ScriptableObject
{
    public string cardName;

    [TextArea]
    public string description;

    public Sprite icon;

    public int price;

    public CardType type;

    public int value;
}

public enum CardType
{
    Attack,
    Defense,
    Health,
    Buff,
    Debuff,
    Special
}