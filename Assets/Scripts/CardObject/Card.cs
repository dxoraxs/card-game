using System;
using UnityEngine;

[RequireComponent(typeof(CardView)), RequireComponent(typeof(CardMouseHandler))]
public class Card : MonoBehaviour, IPoolObject
{
    [SerializeField] private CardView view;
    [SerializeField] private CardMouseHandler mouseHandler;
    
    public CardView CardView => view;
    public CardMouseHandler MouseHandler => mouseHandler;

    public void SetData(CardDTO data)
    {
        view.SetData(data);
        mouseHandler.SetData(data);
    }
    
    public void OnReturnToPool()
    {
        view.RestartView();
        mouseHandler.RestartHandler();
    }
}