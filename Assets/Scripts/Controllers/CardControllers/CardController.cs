using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all information about card in players hand
/// </summary>
[System.Serializable]
public class CardController
{
    ///<value>holds instance of player holding card</value>
    public PlayerController playerHoldingCard;

    ///<value>holds information about this card</value>
    public CardAsset cardAsset;

    ///<value>gets this cards ID</value>
    public int CardID { get; private set; }

    ///<value>gets this cards ID</value>
    public int ID
    {
        get { return CardID; }
    }

    ///<value>Dictionary holding all cards created this game</value>
    public static Dictionary<int, CardController> CardsCreatedThisGame = new Dictionary<int, CardController>();

    ///<value>Gets information if this card can be played at the moment - checks if board is full and if card is persistent on front row - not persistant cards - spells can be played even if board is full</value>
    public bool CanBePlayedOnFrontRow
    {
        get
        {
            bool ownersTurn = (GameManager.Instance.WhoseTurn == playerHoldingCard);
            bool isCurrentRowFull = false;
            isCurrentRowFull = CheckIfCardCanBePlayedInThisRow(isCurrentRowFull);
            return ownersTurn && isCurrentRowFull;
        }
    }

    ///<value>Gets information if this card can be played at the moment - checks if board is full and if card is persistent on back row - not persistant cards - spells can be played even if board is full</value>
    public bool CanBePlayedOnBackRow
    {
        get
        {
            bool ownersTurn = (GameManager.Instance.WhoseTurn == playerHoldingCard);
            bool isCurrentRowFull = false;
            isCurrentRowFull = CheckIfCardCanBePlayedInThisRow(isCurrentRowFull);
            return ownersTurn && isCurrentRowFull;
        }
    }

    ///<value>if isPersistant is false and isCreature is false - this means this card is a spell that can be played even if board is full</value>
    private bool CheckIfCardCanBePlayedInThisRow(bool isCurrentRowFull)
    {
        if (cardAsset.isPersistent || cardAsset.isCreature)
        {
            isCurrentRowFull = (playerHoldingCard.frontRow.CardsOnFrontRow.Count < 7);
        }

        return isCurrentRowFull;
    }
    public CardController(CardAsset card)
    {
        cardAsset = card ?? throw new ArgumentNullException(nameof(card));
        CardID = IDCreator.CreateUniqueID();
        CardsCreatedThisGame.Add(CardID, this);
    }
}
