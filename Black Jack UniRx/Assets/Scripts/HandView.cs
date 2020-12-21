using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField]
    private CardView m_CardPrefab;
    [SerializeField]
    private RectTransform m_FlyCardPrefab;

    [SerializeField]
    private RectTransform m_Content;
    [SerializeField]
    private RectTransform m_AnimationStartTransform;
    [SerializeField]
    private RectTransform m_AnimationEndTransform;

    private List<CardView> m_Hand =  new List<CardView>();
    private Queue<Action> m_Animations = new Queue<Action>();

    private void Awake()
    {
        m_CardPrefab.gameObject.SetActive(false);
        m_FlyCardPrefab.gameObject.SetActive(false);
    }

    private void Start()
    {
        Observable.Timer (TimeSpan.FromSeconds (.2f)) 
            .Repeat ()
            .Subscribe (_ => 
            {
                if (m_Animations.Count >0)
                {
                    m_Animations.Dequeue().Invoke();
                }
                
            });
    }

    public void AddCard(CardData cardValue, bool show = true)
    {
        m_Animations.Enqueue(() =>
        {
            
            var cardView = Instantiate(m_CardPrefab, m_Content);
            cardView.SetImage(cardValue.Icon);
            m_Hand.Add(cardView);
            
            var flyCard = Instantiate(m_FlyCardPrefab,m_AnimationStartTransform);
            flyCard.transform.position = m_AnimationStartTransform.position;
            flyCard
                .DOMove(m_AnimationEndTransform.position, .5f)
                .OnStart(()=>
                {
                    flyCard.gameObject.SetActive(true);
                })
                .OnComplete(() =>
                {
                    cardView.gameObject.SetActive(true);
                    Destroy(flyCard.gameObject);
                });
        });
        
        
    }
    
    
   


    public void Clear()
    {
        foreach (var view in m_Hand)
            Destroy(view.gameObject);
        m_Hand.Clear();
    }
    
}