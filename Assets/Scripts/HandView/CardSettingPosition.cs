using System;
using UnityEngine;

[Serializable]
public class CardSettingPosition
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