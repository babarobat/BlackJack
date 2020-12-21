using UniRx;

public class O4koModel
{
    public ReactiveProperty<int> Deposit { get; }
    public ReactiveProperty<int> Bid { get; }
    
    public ReactiveCollection<Card> PlayerHand { get; }
    
    public ReactiveCollection<Card> CasinoHand { get; }
    
    public ReactiveProperty<Deck> CurrentDeck { get; }
    

    public O4koModel(int startDeposit)
    {
        Deposit = new ReactiveProperty<int>(startDeposit);
        Bid = new ReactiveProperty<int>();
        
        PlayerHand = new ReactiveCollection<Card>();
        CasinoHand = new ReactiveCollection<Card>();
        CurrentDeck = new  ReactiveProperty<Deck>(Deck.Empty());

    }
}