using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMouseHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Action<Card> onPointerEnter;
    private Action<Card> onPointerExit;
    private Card card;

    public void SetData(CardDTO data)
    {
        data.SubscribeActionDeath(RestartHandler);
    }

    public void RestartHandler()
    {
        onPointerEnter = null;
        onPointerExit = null;
        card = null;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(card);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke(card);
    }

    public void Initialize(Action<Card> onEnter, Action<Card> onExit, Card card)
    {
        onPointerEnter = onEnter;
        onPointerExit = onExit;
        this.card = card;
    }
}