using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TableCardController : CardViewController
{
    private const float TIME_TO_ANIMATION_OUTLINE = .25F;
    [SerializeField] private CardsDragController dragController;
    [Space, SerializeField] private RectTransform backgroundTableImage;
    [SerializeField] private Image outline;
    private Tween outlineAnimation;
    private bool lastValueDrag;

    public bool IsCardSetToTable => lastValueDrag;
    public void DisableOutline() => StartAnimation(false);

    public override void AddCard(int index, Card card)
    {
        base.AddCard(index, card);
        card.MouseHandler.RestartHandler();
        card.CardView.SetToTable();
    }

    private void Start()
    {
        outline.color -= new Color(0, 0, 0, outline.color.a);
        dragController.SubscribeDragCard(OnDragMovement);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var barrierYPosition = backgroundTableImage.position - Vector3.up * backgroundTableImage.rect.height / 2;
        Gizmos.DrawLine(barrierYPosition + Vector3.left * backgroundTableImage.rect.width / 2,
            barrierYPosition + Vector3.right * backgroundTableImage.rect.width / 2);
    }
#endif

    private void OnDragMovement(Vector3 newPosition)
    {
        var bottomBarrier = backgroundTableImage.position.y - backgroundTableImage.rect.height / 2f;

        StartAnimation(bottomBarrier < newPosition.y);
    }

    private void StartAnimation(bool value)
    {
        if (lastValueDrag == value)
        {
            return;
        }

        lastValueDrag = value;
        outlineAnimation?.Kill();
        outlineAnimation = outline.DOFade(value ? 1 : 0, TIME_TO_ANIMATION_OUTLINE);
    }
}