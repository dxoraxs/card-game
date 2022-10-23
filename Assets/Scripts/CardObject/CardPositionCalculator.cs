using UnityEngine;

public abstract class CardPositionCalculator : MonoBehaviour
{
    [SerializeField] protected float distanceBetweenCards;

    public abstract void ResetCardPosition(Card[] cards);
}