using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDataManager : MonoBehaviour
{
    [SerializeField] private CardData[] data;
    [SerializeField] private string urlImageLoad;
    [SerializeField] private Vector2 sizeImageCard;
    [SerializeField] private ProgressBarController progressBar;
    private CardDTO[] cards;
    private readonly List<int> freeCardIndexs = new();

    public bool GetNewDataCard(out CardDTO data, out int index)
    {
        if (freeCardIndexs.Count == 0)
        {
            index = -1;
            data = null;
            return false;
        }

        index = freeCardIndexs[Random.Range(0, freeCardIndexs.Count)];
        data = cards[index];
        freeCardIndexs.Remove(index);
        return true;
    }

    public CardDTO this[int index] => cards[index];

    public IEnumerator LoadTextures(Action onEndLoad)
    {
        cards = new CardDTO[data.Length];
        for (var index = 0; index < data.Length; index++)
        {
            freeCardIndexs.Add(index);
            var cardData = data[index];
            var newCardDto = new CardDTO(cardData);
            cards[index] = newCardDto;
            yield return NetworkTextureDownloader.DownloadImage(
                urlImageLoad + (sizeImageCard.x + index / 2) + "/" + (sizeImageCard.y + index % 2 + index / 2),
                texture => { OnTexturesLoad(texture, newCardDto, index); });
        }

        OnEndTexturesLoad();
        onEndLoad?.Invoke();
    }

    private void OnTexturesLoad(Texture texture, CardDTO newCardDto, int index)
    {
        texture.name = newCardDto.Data.name + "_texture";
        newCardDto.Texture = texture;
        ChangeProgressBar((float)(index + 1) / data.Length);
    }

    private void OnEndTexturesLoad()
    {
        progressBar.HidePanel();
    }

    private void ChangeProgressBar(float percent)
    {
        progressBar.SetNewValue(percent);
    }
}