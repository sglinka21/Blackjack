using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck 
{
    private Card[] cards = new Card[52];
    private int index;

    public Deck()
    {
        foreach (Suits suit in (Suits[])System.Enum.GetValues(typeof(Suits)))
        {
            for (int rank = 1; rank <= 13; rank++)
            {
                cards[(int)suit * 13 + rank - 1] = new Card(suit, rank); 
            }
        }
        ShuffleCard();
    }

    public void ShuffleCard()
    {
        index = 0;
        for (int i = 0; i < 51; i++)
        {
            int randIndex = Random.Range(i, 52);
            Card temp = cards[i];
            cards[i] = cards[randIndex];
            cards[randIndex] = temp;
        }
    }

    public Card DealCard()
    {
        if(index > 51)
        {
            ShuffleCard();
        }
        return cards[index++];
    }
}
