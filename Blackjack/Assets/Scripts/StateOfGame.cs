using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateOfGame 
{    
    Betting,
    DealFirstCardToPlayer,
    DealSecondCardToPlayer,
    CheckBlackjack,
    DealTwoCardsToDealer,
    WaitForPlayerToAct,
    WaitForDealerToAct,
    ProcessResult,
    WaitForRestart   
}
