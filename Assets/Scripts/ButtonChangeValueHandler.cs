using System;
using UnityEngine;

public class ButtonChangeValueHandler : MonoBehaviour
{
    private Action onClickChangeButton;

    public void SubscribeChangeValue(Action callback) => onClickChangeButton += callback;
    public void OnClickButton() => onClickChangeButton?.Invoke();
}