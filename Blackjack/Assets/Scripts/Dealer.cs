using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer 
{

    public Hand Hand { get; set; }
   
    public Dealer ()
    {
        Hand = new Hand();
    }
}
