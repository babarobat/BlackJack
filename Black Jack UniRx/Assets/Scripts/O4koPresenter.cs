using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

public class O4koPresenter
{
    private readonly O4koView m_View;
    private readonly O4koModel m_Model;

    private readonly CompositeDisposable m_Disposables;

    public O4koPresenter(O4koView view, O4koModel model)
    {
        m_View = view;
        m_Model = model;
        m_Disposables = new CompositeDisposable();
    }

    public void Init()
    {
        m_Model.Bid.SubscribeToText(m_View.BidText);
        m_Model.Deposit.SubscribeToText(m_View.DepositText);


       
        m_Model.CasinoHand.ObserveReset().Subscribe(_ =>
        {
            m_View.DealerValue.text = string.Empty;
            m_View.ClearCasinoHand();
           
        });
        m_Model.CasinoHand
            .ObserveAdd()
            .Do(card =>
            {
                m_View.AddCardToDealersHand(card.Value);
            })
            .Subscribe(card => { m_View.DealerValue.text = ConvertToString(m_Model.CasinoHand.ToList()); });
        
        m_Model.PlayerHand.ObserveReset().Subscribe(_ =>
        {
            m_View.PlayerValue.text = string.Empty;
            m_View.ClearPlayerHand();
        });
        m_Model.PlayerHand
            .ObserveAdd()
            .Do(card =>
            {
                m_View.AddCardToPlayersHand(card.Value);
            })
            .Subscribe(card => { m_View.PlayerValue.text = ConvertToString(m_Model.PlayerHand.ToList()); });


        m_View.StartRoundButton
            .OnClickAsObservable()
            .Subscribe(_ => StartRound());
        m_View.TakeCardButton
            .OnClickAsObservable()
            .Subscribe(_ => TakeCard());

        m_View.IncreaseBidButton
            .OnClickAsObservable()
            .Subscribe(_ => m_Model.Bid.Value += 1);

        m_View.DecreaseBidButton
            .OnClickAsObservable()
            .Subscribe(_ =>
            {
                if (m_Model.Bid.Value > 0)
                    m_Model.Bid.Value -= 1;
            });
        m_View.StopButton
            .OnClickAsObservable()
            .Subscribe(_ => Finish());
    }

    private void StartRound()
    {
        if (m_Model.Deposit.Value <= m_Model.Bid.Value)
        {
            //Not enough money
            Debug.Log("Not enough money");
            return;
        }

        m_Model.Deposit.Value -= m_Model.Bid.Value;
       

        m_Model.CasinoHand.Clear();
        m_Model.PlayerHand.Clear();
        m_Model.CurrentDeck.SetValueAndForceNotify(Deck.Create());

        for (var i = 0; i < 2; i++)
        {
            m_Model.PlayerHand.Add(m_Model.CurrentDeck.Value.GetCard());
            m_Model.CasinoHand.Add(m_Model.CurrentDeck.Value.GetCard());
        }

        var playerResult = GetSum(m_Model.PlayerHand);
        var dealerResult = GetSum(m_Model.CasinoHand);

        if (playerResult > 21)
        {
            //loose
            Debug.Log("Loose");
            return;
        }
        else if (playerResult == 21)
        {
            //loose
            Debug.Log("Win");
            return;
        }
    }

    private void TakeCard()
    {
        m_Model.PlayerHand.Add(m_Model.CurrentDeck.Value.GetCard());
        var playerResult = GetSum(m_Model.PlayerHand);
        if (playerResult > 21)
        {
            //loose
            Debug.Log("Loose");
            Debug.Log($"playerResult {playerResult}");
            return;
        }
        else if (playerResult == 21)
        {
            Win();
            Debug.Log("Win");
            return;
        }
    }

    private void Win()
    {
        m_Model.Deposit.Value += m_Model.Bid.Value * 2;
    }

    private void Finish()
    {
        var playSum = GetSum(m_Model.PlayerHand);
        while (GetSum(m_Model.CasinoHand) <= playSum)
        {
            m_Model.CasinoHand.Add(m_Model.CurrentDeck.Value.GetCard());
        }
        var dealerResult = GetSum(m_Model.CasinoHand);
        if (dealerResult > 21  || playSum > dealerResult)
        {
            Win();
            Debug.Log("Win");
            return;
        }

        

        Debug.Log("Loose");
    }

    private string ConvertToString(IReadOnlyList<Card> cards)
    {
        var sum = 0;
        var sb = new StringBuilder();
        for (var index = 0; index < cards.Count; index++)
        {
            sb.Append(cards[index].StringValue);
            if (index != cards.Count - 1)
                sb.Append("+");
            sum += cards[index].Value;
        }

        sb.Append("=");
        sb.Append(sum);
        return sb.ToString();
    }

    private int GetSum(IEnumerable<Card> cards)
    {
        return cards.Sum(card => card.Value);
    }
}