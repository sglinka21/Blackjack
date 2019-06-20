using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public StateOfGame StateOfGame { set; get; }

    public object Data { set; get; }

    public GameState(StateOfGame state, object data)
    {
        StateOfGame = state;
        Data = data;
    }

    public GameState(StateOfGame state) : this(state, null)
    {
    }
}
