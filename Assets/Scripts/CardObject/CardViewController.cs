using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardViewController : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;
    [SerializeField] private CardPositionCalculator positionCalculator;
    private readonly Dictionary<int,Card> cardsInHand = new();

    public int[] GetIndexesCard => cardsInHand.Keys.ToArray();

    public bool IsHaveThisCard(Card card)
    {
        return cardsInHand.Values.ToList().Contains(card);
    }
    
    public virtual void AddCard(int index, Card card)
    {
        card.transform.SetParent(parentTransform);
        cardsInHand.Add(index, card);
        positionCalculator.ResetCardPosition(cardsInHand.Values.ToArray());
    }

    public int GetIndexFromCard(Card card)
    {
        var indexOf = cardsInHand.Values.ToList().IndexOf(card);
        return cardsInHand.ElementAt(indexOf).Key;
    }

    public void ReturnCardFromPreview(Card card)
    {
        card.transform.SetParent(parentTransform);
        card.transform.SetSiblingIndex(cardsInHand.Values.ToList().IndexOf(card));
        positionCalculator.ResetCardPosition(cardsInHand.Values.ToArray());
    }

    public void RemoveCard(int index, out Card card)
    {
        cardsInHand.Remove(index, out card);
        positionCalculator.ResetCardPosition(cardsInHand.Values.ToArray());
    }
}