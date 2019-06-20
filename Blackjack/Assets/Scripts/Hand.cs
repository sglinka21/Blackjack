using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand 
{
    private readonly List<Card> cards;
    public List<Card> Cards { get { return cards; } }

    public Hand()
    {
        cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void Clear()
    {
        cards.Clear();
    }

    public int Size
    {
        get
        {
            return cards.Count;
        }
    }
}
