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

    public bool IsCardAllowedToBePlayed { get; set; }

    internal void OnTurnStart()
    {
    }

    internal void PlayCardFromHand(CardController chosenCard, int chosenRow)
    {
        if (IsCardAllowedToBePlayed)
        {

        }
    }
}
