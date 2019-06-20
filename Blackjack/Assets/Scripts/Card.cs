using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private string[] suits = { "spades", "hearts", "diamonds", "clubs" };
    public readonly Suits suit;
    private int rank;

    public int Rank { get { return rank; } }

    public Card(Suits suit, int rank)
    {
        this.suit = suit;
        this.rank = rank;
    }

    public override string ToString()
    {
        return suits[(int)suit] + "_" + rank;
    }


}
