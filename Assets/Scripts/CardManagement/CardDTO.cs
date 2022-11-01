using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardDTO
{
    private UpdatedValue<int> mana;
    private UpdatedValue<int> health;
    private UpdatedValue<int> damage;
    private CardData data;
    private Texture texture;
    private Action onCardDeath;

    public CardDTO(CardData data)
    {
        mana = new UpdatedValue<int>(data.Mana);
        health = new UpdatedValue<int>(data.Health);
        damage = new UpdatedValue<int>(data.Damage);
        this.data = data;
    }

    public Texture Texture
    {
        get => texture;
        set => texture = value;
    }

    public void SubscribeActionDeath(Action value) => onCardDeath += value;

    public void SetHealth(int value)
    {
        health.Value = value;
        if (value < 1)
        {
            onCardDeath?.Invoke();
        }
    }
    public UpdatedValue<int> Mana => mana;
    public UpdatedValue<int> Health => health;
    public UpdatedValue<int> Damage => damage;
    public CardData Data => data;
}