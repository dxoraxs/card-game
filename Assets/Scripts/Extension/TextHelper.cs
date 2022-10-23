using System.Collections;
using TMPro;
using UnityEngine;

public static class TextHelper
{
    private const float TIME_DELAY_TEXT_VALUE = 0.1F;
    
    public static IEnumerator TextCounter(this TMP_Text text, int newValue, int oldValue)
    {
        while (oldValue < newValue)
        {
            oldValue++;
            text.text = oldValue.ToString();
            yield return new WaitForSeconds(TIME_DELAY_TEXT_VALUE);
        }

        text.text = newValue.ToString();
    }
}