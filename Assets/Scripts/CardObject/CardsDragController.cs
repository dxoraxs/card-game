using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDragController : MonoBehaviour
{
    [SerializeField] private PreviewCardController previewCardController;
    [SerializeField] private CardViewController handCardController;
    [SerializeField] private TableCardController tableCardController;
    private Action<Vector3> onDragCard;
    private Card dragCard;
    private DragType currentType;
    private Vector3 startMousePosition;
    private Vector3 startCardPosition;

    private enum DragType
    {
        NONE,
        DRAG
    }

    public void SubscribeDragCard(Action<Vector3> action) => onDragCard += action;

    public void InitializeCard(Card card)
    {
        card.MouseHandler.Initialize(OnMouseEnter, OnMouseExit, card);
    }

    private void Update()
    {
        switch (currentType)
        {
            case DragType.NONE:
                if (Input.GetMouseButtonDown(0) && dragCard != null && handCardController.IsHaveThisCard(dragCard))
                {
                    StartDragCard();
                }
                break;
            case DragType.DRAG:
                MoveCard();
                if (Input.GetMouseButtonUp(0))
                {
                    StopDrag();
                }
                break;
        }
    }

    private void MoveCard()
    {
        dragCard.transform.localPosition = startCardPosition + (GetMousePosition() - startMousePosition);
        onDragCard?.Invoke(dragCard.transform.position);
    }

    private static Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    private void StopDrag()
    {
        currentType = DragType.NONE;
        dragCard.CardView.OnUnsetPreview();
        
        if (tableCardController.IsCardSetToTable)
        {
            var indexCard = handCardController.GetIndexFromCard(dragCard);
            tableCardController.AddCard(indexCard, dragCard);
            tableCardController.DisableOutline();
            handCardController.RemoveCard(indexCard, out dragCard);
        }
        else
        {
            handCardController.ReturnCardFromPreview(dragCard);
        }
    }

    private void StartDragCard()
    {
        dragCard.CardView.StopMove();
        currentType = DragType.DRAG;

        startMousePosition = GetMousePosition();

        startCardPosition = dragCard.transform.localPosition;
    }

    private void OnMouseEnter(Card card)
    {
        dragCard = card;
        card.CardView.EnableOutline();
        card.CardView.OnSetPreview();
        previewCardController.SetCard(card);
    }

    private void OnMouseExit(Card card)
    {
        StopDrag();
        dragCard = null;
    }
}