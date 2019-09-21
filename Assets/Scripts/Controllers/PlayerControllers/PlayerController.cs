using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerID;

    public DeckController deck;
    public HandController hand;
    public FrontRowController frontRow;
    public BackRowController backRow;

    ///<value>Holds both players in an array with maximum two values 0 or 1</value>
    public static PlayerController[] Players;

    public bool IsCardAllowedToBePlayed { get; set; }

    public PlayerController otherPlayer
    {
        get
        {
            if (Players[0] == this)
                return Players[1];
            else
                return Players[0];
        }
    }
    void Awake()
    {
        Players = FindObjectsOfType<PlayerController>();
        PlayerID = IDCreator.CreateUniqueID();
    }

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
