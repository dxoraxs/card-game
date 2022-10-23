using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Data", fileName = "Card")]
public class CardData : ScriptableObject
{
    public string Name;
    public string Description;
    [Space] public int Health;
    public int Damage;
    public int Mana;
}