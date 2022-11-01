using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    private const float TIME_TO_ANIMATION_OUTLINE = .25F;
    private const float TIME_MOVE_CARD = 1F;
    private const float TIME_ROTATE_CARD = 1F;
    private const float TIME_FADE_CARD = 1F;
    private const float TIME_DELAY_TO_FADE_CARD = 1F;
    private const float OFFSET_UP_MOVE_ON_FADE = 100F;

    [SerializeField] private CanvasGroup fadeCard;
    [SerializeField] private Image outline;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text description;
    [SerializeField] private CardText mana;
    [SerializeField] private CardText health;
    [SerializeField] private CardText damage;
    [SerializeField] private RawImage image;
    [Space, SerializeField] private UIComponents[] allUIComponents;
    [SerializeField] private Color deathColor;
    private Tween localMove;
    private Tween localRotate;

    public void StopMove()
    {
        localMove.Kill();
    }

    public void SetToTable()
    {
        transform.DOScale(.8f, TIME_MOVE_CARD);
    }

    public void OnSetPreview()
    {
        transform.DOScale(1.1f, TIME_TO_ANIMATION_OUTLINE);
        LocalRotate(Vector3.zero);
        LocalMove(Vector3.right * transform.localPosition.x - Vector3.up * outline.rectTransform.rect.y);
    }

    public void OnUnsetPreview()
    {
        transform.DOScale(1f, TIME_TO_ANIMATION_OUTLINE);
        DisableOutline();
    }

    public void EnableOutline() => outline.DOFade(1, TIME_TO_ANIMATION_OUTLINE);
    public void DisableOutline() => outline.DOFade(0, TIME_TO_ANIMATION_OUTLINE);

    public void RestartView()
    {
        fadeCard.alpha = 1;
        outline.color -= new Color(0, 0, 0, 1);
        transform.localScale =Vector3.one;
        localMove.Kill();
        localRotate.Kill();
        foreach (var uiComponent in allUIComponents)
        {
            uiComponent.ReturnToDefault();
        }
    }

    public void LocalMove(Vector3 newPosition, float duration = TIME_MOVE_CARD)
    {
        localMove?.Kill();
        localMove = transform.DOLocalMove(newPosition, TIME_MOVE_CARD);
    }

    public void LocalRotate(Vector3 newRotate)
    {
        localRotate?.Kill();
        localRotate = transform.DOLocalRotate(newRotate, TIME_ROTATE_CARD);
    }

    public void SetData(CardDTO data)
    {
        data.SubscribeActionDeath(OnDeathCard);
        image.texture = data.Texture;
        name.text = data.Data.Name;
        description.text = data.Data.Description;
        data.Mana.SubscribeChanges(SetManaText);
        data.Health.SubscribeChanges(SetHealthText);
        data.Damage.SubscribeChanges(SetDamageText);
    }

    private void Fade()
    {
        fadeCard.DOFade(0, TIME_FADE_CARD).OnComplete(() => gameObject.SetActive(false));
        LocalMove(transform.localPosition + Vector3.up * OFFSET_UP_MOVE_ON_FADE, TIME_FADE_CARD);
    }

    private void SetManaText(int newValue) => mana.SetText(this, newValue);
    private void SetHealthText(int newValue) => health.SetText(this, newValue);
    private void SetDamageText(int newValue) => damage.SetText(this, newValue);

    private void Start()
    {
        outline.color -= new Color(0, 0, 0, 1);
    }

    private void OnDeathCard()
    {
        transform.DOLocalRotate(Vector3.zero, TIME_DELAY_TO_FADE_CARD);
        DisableOutline();
        DOVirtual.DelayedCall(TIME_DELAY_TO_FADE_CARD, Fade);
        foreach (var uiComponent in allUIComponents)
        {
            uiComponent.SetNewColor(deathColor, TIME_DELAY_TO_FADE_CARD / 2f);
        }
    }
}