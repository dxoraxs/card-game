using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameCardController : MonoBehaviour
{
    [SerializeField] private CardViewController handViewController;
    [SerializeField] private CardPoolController poolController;
    [SerializeField] private CardDataManager dataManager;
    [SerializeField] private CardsDragController dragController;
    [Space, SerializeField] private int minCountOnStart;
    [SerializeField] private int maxCountOnStart;
    private readonly List<int> indexesCardsInHand = new();

    public CardDTO[] GetCardsInHand()
    {
        var cardIndexInHand = handViewController.GetIndexesCard;
        var result = new CardDTO[cardIndexInHand.Length];
        for (int index = 0; index < cardIndexInHand.Length; index++)
        {
            result[index] = dataManager[cardIndexInHand[index]];
        }

        return result;
    }

    private void Start()
    {
        StartCoroutine(dataManager.LoadTextures(GetCardToHand));
    }

    private void GetCardToHand()
    {
        var startCount = Random.Range(minCountOnStart, maxCountOnStart + 1);
        for (int count = 0; count < startCount; count++)
        {
            GetNewCardToHand();
        }
    }

    private void GetNewCardToHand()
    {
        if (!dataManager.GetNewDataCard(out var data, out var index))
        {
            return;
        }

        indexesCardsInHand.Add(index);
        var card = poolController.GetFreeCard;
        dragController.InitializeCard(card);
        card.SetData(data);
        handViewController.AddCard(index, card);
        data.SubscribeActionDeath(() => OnCardDeath(index));
    }

    private void OnCardDeath(int index)
    {
        indexesCardsInHand.Remove(index);
        handViewController.RemoveCard(index, out var card);
        poolController.ReturnCardPool(card);
    }
}