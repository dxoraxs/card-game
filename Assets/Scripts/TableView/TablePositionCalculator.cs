using UnityEngine;

public class TablePositionCalculator : CardPositionCalculator
{
    [SerializeField] private RectTransform backgroundRectTransform;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < 6; i++)
        {
            var cardPosition = backgroundRectTransform.position - Vector3.right * (distanceBetweenCards * (i - (2.5f)));
            Gizmos.DrawLine(cardPosition + Vector3.left * 100, cardPosition + Vector3.right * 100);
            Gizmos.DrawLine(cardPosition + Vector3.up * 100, cardPosition + Vector3.down * 100);
        }
    }
#endif

    public override void ResetCardPosition(Card[] cards)
    {
        var positions = CalculatePositions(cards.Length);

        for (int index = 0; index < positions.Length; index++)
        {
            cards[index].CardView.LocalMove(positions[index]);
        }
    }

    private Vector3[] CalculatePositions(int count)
    {
        var positions = new Vector3[count];

        for (int index = 0; index < count; index++)
        {
            var cardPosition = backgroundRectTransform.position -
                               Vector3.right * (distanceBetweenCards * (index - (count / 2f - 0.5f)));
            positions[index] = cardPosition;
        }

        return positions;
    }
}