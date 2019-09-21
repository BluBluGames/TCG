using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public DeckController deck;
    public HandController hand;
    public FrontRowController frontRow;
    public BackRowController backRow;

    internal void OnTurnStart()
    {
    }

    internal void PlayCardFromHand(CardController chosenCard, int v)
    {
    }
}
