using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIComponents
{
    [SerializeField] private Graphic component;
    [SerializeField] private Color _default;

    public UIComponents(Graphic component, Color @default)
    {
        this.component = component;
        _default = @default;
    }

    public void ReturnToDefault() => component.color = _default;
    public void SetNewColor(Color color, float duration) => component.DOColor(color, duration);
}