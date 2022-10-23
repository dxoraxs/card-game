using System;
using System.Collections.Generic;
using UnityEngine;

public class HandPositionCalculator : CardPositionCalculator
{
    [SerializeField] private CardSettingPosition minCount;
    [SerializeField] private CardSettingPosition maxCount;

    public override void ResetCardPosition(Card[] cards)
    {
        var cardSettingPosition = CardSettingPosition.CreateBetweenSettings(cards.Length, minCount, maxCount);
        CardPosition(cardSettingPosition, out var positions, out var centerPosition);

        for (int index = 0; index < positions.Length; index++)
        {
            cards[index].CardView.LocalMove(centerPosition + positions[index] * cardSettingPosition.Radius);
            cards[index].CardView.LocalRotate(Quaternion.Euler(Vector3.forward * Vector3.SignedAngle( 
                Vector3.up, positions[index], Vector3.forward)).eulerAngles);
        }
    }

    [Serializable] private class CardSettingPosition
    {
        public float Radius;
        public float YOffsetPosition;
        public int Count;

        public CardSettingPosition(float radius, float yOffsetPosition, int count)
        {
            Radius = radius;
            YOffsetPosition = yOffsetPosition;
            Count = count;
        }

        public static CardSettingPosition CreateBetweenSettings(int countCard, CardSettingPosition minSetting,
            CardSettingPosition maxSetting)
        {
            var setting = (float)(countCard - minSetting.Count) / (maxSetting.Count - minSetting.Count);
            return new CardSettingPosition(Mathf.Lerp(minSetting.Radius, maxSetting.Radius, setting),
                Mathf.Lerp(minSetting.YOffsetPosition, maxSetting.YOffsetPosition, setting),
                countCard);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = GetComponentInParent<RectTransform>().localToWorldMatrix;
        Gizmos.color = Color.red;
        DrawCardSetting(minCount);
        Gizmos.color = Color.blue;
        DrawCardSetting(maxCount);
    }
    
    private void DrawCardSetting(CardSettingPosition setting)
    {
        var centerCirclePosition = Vector3.up * setting.YOffsetPosition;
        var cardPositions = CalculatePositions(setting);

        Gizmos.DrawLine(
            centerCirclePosition + Quaternion.Euler(Vector3.forward * 5) * cardPositions[0] * setting.Radius,
            centerCirclePosition + cardPositions[0] * setting.Radius);

        for (int i = 0; i < cardPositions.Length; i++)
        {
            if (i < cardPositions.Length - 1)
                Gizmos.DrawLine(centerCirclePosition + cardPositions[i] * setting.Radius,
                    centerCirclePosition + cardPositions[i + 1] * setting.Radius);
            Gizmos.DrawLine(centerCirclePosition + cardPositions[i] * setting.Radius * 1.1f,
                centerCirclePosition + cardPositions[i] * setting.Radius * 0.9f);
        }

        Gizmos.DrawLine(
            centerCirclePosition + Quaternion.Euler(Vector3.forward * -5) * cardPositions[cardPositions.Length - 1] *
            setting.Radius,
            centerCirclePosition + cardPositions[cardPositions.Length - 1] * setting.Radius);
    }
    #endif

    private void CardPosition(CardSettingPosition setting, out Vector3[] position, out Vector3 centerPosition)
    {
        centerPosition = Vector3.up * setting.YOffsetPosition;
        position = CalculatePositions(setting);
    }

    private Vector3[] CalculatePositions(CardSettingPosition setting)
    {
        var lenghtCircle = 2 * Mathf.PI * setting.Radius;
        var angleOffsetCards = (360 * distanceBetweenCards) / lenghtCircle;
        var cardPosition = Quaternion.Euler(Vector3.forward * angleOffsetCards * (setting.Count / 2f - .5f)) *
                           Vector3.up;
        var positions = new List<Vector3>();

        for (int i = 0; i < setting.Count; i++)
        {
            positions.Add(cardPosition);
            cardPosition = Quaternion.Euler(Vector3.back * angleOffsetCards) * cardPosition;
        }

        return positions.ToArray();
    }
}