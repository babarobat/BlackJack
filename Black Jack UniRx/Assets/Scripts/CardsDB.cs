using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Create cards db",fileName = "Cards DB"),]
public class CardsDB : ScriptableObject
{
    [SerializeField]
    private CardData[] m_Cards = new CardData [0];

    public CardData GetData(Card card)
    {
        return m_Cards.FirstOrDefault(d => d.Suit == card.Suit && d.Value == card.Value);
    }
}