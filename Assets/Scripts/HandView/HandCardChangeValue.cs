using UnityEngine;
using Random = UnityEngine.Random;

public class HandCardChangeValue : MonoBehaviour
{
    [SerializeField] private ButtonChangeValueHandler changeValueHandler;
    [SerializeField] private GameCardController cardController;
    private int counter = 0;

    private void Start()
    {
        changeValueHandler.SubscribeChangeValue(OnClickChangeValueOnCard);
    }

    private void OnClickChangeValueOnCard()
    {
        var cardsInHand = cardController.GetCardsInHand();

        if (cardsInHand.Length == 0)
        {
            return;
        }

        var currentData = cardsInHand[counter % cardsInHand.Length];
        var newRandomValue = Random.Range(-2, 9);
        switch (Random.Range(0, 3))
        {
            case 0:
                currentData.Damage.Value = newRandomValue;
                break;
            case 1:
                currentData.SetHealth(newRandomValue);
                if (newRandomValue < 1) counter--;
                break;
            case 2:
                currentData.Mana.Value = newRandomValue;
                break;
        }

        counter++;
        if (cardsInHand.Length <= counter)
        {
            counter = 0;
        }
    }
}