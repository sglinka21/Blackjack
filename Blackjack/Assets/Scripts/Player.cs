using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int bet;
    private int cash;

    public int Bet { get { return bet; } }

    public string Name { get; set; }

    public int Cash
    {
        get
        {
            return cash;
        }
        set
        {
            if (value < 0)
            {
                cash = 0;
            }
            else
            {
                cash = value;
            }
        }

    }

    public Hand Hand { get; set; }

    public Player(string name, int cash)
    {
        bet = 0;
        Hand = new Hand();
        Name = name;
        this.cash = cash;
    }

    public bool BetCash(int amount)
    {
        if (amount <= 0)
        {

        }
        if (cash < amount)
        {
            return false;
        }

        cash -= amount;
        bet += amount;
        return true;
    }

    public void AddCash(int amount)
    {
        cash += amount;
    }

    public void ResetBet()
    {
        bet = 0;
    }
}
