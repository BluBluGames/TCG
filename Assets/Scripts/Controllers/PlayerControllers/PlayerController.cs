using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ///<value>Holds ID of this player</value>
    public int PlayerID;

    ///<value>Contains list of cards in players deck</value>
    public DeckController deck;
    ///<value>Contains list of cards in players hand</value>
    public HandController hand;
    ///<value>Contains list of cards on players front row</value>
    public FrontRowController frontRow;
    ///<value>Contains list of cards on players back row</value>
    public BackRowController backRow;

    ///<value>Holds both players in an array with maximum two values 0 or 1</value>
    public static PlayerController[] Players;

    private bool isCardAllowedToBePlayed;
    ///<value>Used to determine if player can play a card this turn - normally player can play one card a turn</value>
    public bool IsCardAllowedToBePlayed { get => isCardAllowedToBePlayed; set => isCardAllowedToBePlayed = value; }


    ///<value>Getter for current player ID</value>
    public int ID
    {
        get { return PlayerID; }
    }

    ///<value>Getter for other player</value>
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
    private int playerHealth;

    ///<value>Gets and Sets players Health</value>
    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }



    /// <summary>
    /// Contains visual information about area of current player
    /// </summary>
    public PlayerView playerView;

    ///<value>Registered game evens</value>
    public delegate void VoidWithNoArguments();
    public event VoidWithNoArguments OnGameEnd;
    public event VoidWithNoArguments EndTurnEvent;

    /// <summary>
    /// On creation every player is given unique ID
    /// </summary>
    void Awake()
    {
        Players = FindObjectsOfType<PlayerController>();
        PlayerID = IDCreator.CreateUniqueID();
    }

    /// <summary>
    /// Method called by turn manager on start of every players turn
    /// </summary>
    public void OnTurnStart()
    {
        if (hand.CardsInHand.Count == 0)
        {
            OnTurnEnd();
            CheckIfGameEnded();
        }

        IsCardAllowedToBePlayed = true;

        foreach (CardOnBoardController card in frontRow.CardsOnFrontRow)
            card.OnTurnStart();

        foreach (CardOnBoardController card in backRow.CardsOnBackRow)
            card.OnTurnStart();
    }
    
    /// <summary>
    /// Method used by turn manager on end of every players turn
    /// </summary>
    public void OnTurnEnd()
    {
        CheckIfGameEnded();

        if (EndTurnEvent != null)
            EndTurnEvent.Invoke();

        GetComponent<TurnController>().StopAllCoroutines();
    }

    public void DrawCard()
    {
        new DrawCardCommand(this).AddToQueue();
    }

    public void PlayCardFromHand(int UniqueID, int chosenRow, int tablePos)
    {
        PlayCardFromHand(CardController.CardsCreatedThisGame[UniqueID], chosenRow, tablePos);
    }

    /// <summary>
    /// calls Command to play a card on table - called by AITurnManager::PlayCardFromHand or DragCardOnBoard::OnEndDrag 
    /// </summary>
    /// <param name="chosenCard"></param>
    /// <param name="chosenRow"></param>
    internal void PlayCardFromHand(CardController chosenCard, int chosenRow, int rowPosition)
    {
        if ((chosenCard.CanBePlayedOnFrontRow || chosenCard.CanBePlayedOnBackRow) && IsCardAllowedToBePlayed)
        {
            IsCardAllowedToBePlayed = false;
            new PlayCardCommand(chosenCard, this, chosenRow, rowPosition).AddToQueue();
        }
    }

    /// <summary>
    /// checks if there are no cards in both players hands and if true ends the game
    /// 
    /// </summary>
    public bool CheckIfGameEnded()
    {
        int cardsInHand = hand.CardsInHand.Count;
        int enemyCardsInHand = otherPlayer.hand.CardsInHand.Count;

        if (cardsInHand == 0 && enemyCardsInHand == 0)
        {
            EndGame();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Executes end game commands
    /// </summary>
    public void EndGame()
    {
        if (OnGameEnd != null)
        {
            OnGameEnd.Invoke();
        }
        new EndGameCommand(this).AddToQueue();
    }
}
