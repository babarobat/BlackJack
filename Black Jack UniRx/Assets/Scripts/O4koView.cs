using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class O4koView: MonoBehaviour
{
    [SerializeField]
    private Button m_IncreaseBidButton;
    [SerializeField]
    private Button m_DecreaseBidButton;
    [SerializeField]
    private Button m_StartRoundButton;
    [SerializeField]
    private Button m_TakeCardButton;
    [SerializeField]
    private Button m_StopButton;
    

    

    [SerializeField]
    private TextMeshProUGUI bidText;

    [SerializeField]
    private TextMeshProUGUI depositText;
    [SerializeField]
    private TextMeshProUGUI m_DealerValue;
    [SerializeField]
    private TextMeshProUGUI m_PlayerValue;
    [SerializeField]
    private EndRoundMessageView m_EndRoundMessage;

    [SerializeField]
    private CardsDB m_cardsDb;
    

    [SerializeField]
    private HandView m_PlayerHand;
    [SerializeField]
    private HandView m_DealerHand;

    public Button IncreaseBidButton => m_IncreaseBidButton;

    public Button DecreaseBidButton => m_DecreaseBidButton;

    public Button StartRoundButton => m_StartRoundButton;

    public Button TakeCardButton => m_TakeCardButton;
    public TextMeshProUGUI BidText => bidText;

    public TextMeshProUGUI DepositText => depositText;

    public TextMeshProUGUI DealerValue => m_DealerValue;

    public TextMeshProUGUI PlayerValue => m_PlayerValue;

    public Button StopButton => m_StopButton;


    public void AddCardToPlayersHand(Card cardValue)
    {
        var data = m_cardsDb.GetData(cardValue);
        m_PlayerHand.AddCard(data);
    }
    public void AddCardToDealersHand(Card cardValue)
    {
        var data = m_cardsDb.GetData(cardValue);
        m_DealerHand.AddCard(data);
    }

    public void ClearHands()
    {
        m_PlayerHand.Clear();
        m_DealerHand.Clear();
    }

    public void ClearPlayerHand()
    {
        m_PlayerHand.Clear();
    }

    public void ClearCasinoHand()
    {
        m_DealerHand.Clear();
    }
}