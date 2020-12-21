using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class Deck
{
    public ReactiveCollection<Card> Cards { get; }
    private Deck(List<Card> cards)
    {
        Cards = new ReactiveCollection<Card>(cards); 
    }

    public Card GetCard()
    {
        var card = Cards.Last();
        Cards.RemoveAt(Cards.Count-1);
        return card;
        
    }

    public static Deck Create()
    {
        var newDeck = CreateNew();
        var shuffle = Shuffle(newDeck);
        return new Deck(shuffle.ToList());
    }
    public static Deck Empty()
    {
        return new Deck(new List<Card>());
    }


    private static IEnumerable<Card> CreateNew()
    {
        foreach (var cardValue in Card.Strings.Keys)
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            yield return new Card(cardValue, suit);
    }

    private static IEnumerable<Card> Shuffle(IEnumerable<Card> cards)
    {
        var rand = new Random();
        return cards.OrderBy(x => rand.Next());
    }
}