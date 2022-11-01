using System;
using TMPro;
using UnityEngine;

[Serializable]
public class CardText
{
    [SerializeField] private TMP_Text text;
    private Coroutine animationText;
    private int lastValue;

    public void SetText(MonoBehaviour card, int newValue, bool isAnimation = true)
    {
        if (isAnimation)
        {
            if (animationText != null)
            {
                card.StopCoroutine(animationText);
            }
            
            animationText = card.StartCoroutine(text.TextCounter(newValue, lastValue));
        }
        else
        {
            text.text = newValue.ToString();
        }

        lastValue = newValue;
    }
}