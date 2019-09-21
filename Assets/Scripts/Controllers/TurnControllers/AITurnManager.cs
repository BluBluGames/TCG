using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles playing cards by AI
/// </summary>
public class AITurnManager : TurnManager
{
    /// <summary>
    /// Entry point for turn execution - leads to PlayerController OnStartTUrn()
    /// </summary>
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        ExecuteAITurn();
    }

    /// <summary>
    /// Executes AITurn
    /// </summary>
    private void ExecuteAITurn()
    {
        PlayCardFromHand();
        GameManager.Instance.EndTurn();
    }

    /// <summary>
    /// Chooses between values 0 and 1 - 0 is Front Row, 1 is Back Row
    /// </summary>
    /// <returns></returns>
    private int ChooseRandomRowToPlayCardTo()
    {
        return UnityEngine.Random.Range(0, 2);
    }

    /// <summary>
    /// Plays a card from hand on chosen row
    /// </summary>
    private void PlayCardFromHand()
    {
        CardController chosenCard = ChooseRandomCardFromHand();
        int row = ChooseRandomRowToPlayCardTo();

        if (chosenCard != null)
        {
            player.PlayCardFromHand(chosenCard, row);
        }
    }

    /// <summary>
    /// Chooses Random card from hand
    /// </summary>
    /// <returns></returns>
    private CardController ChooseRandomCardFromHand()
    {
        System.Random generator = new System.Random();
        List<CardController> cardsInHand = GameManager.Instance.whoseTurn.hand.CardsInHand;

        CardController chosenCard = null;
        if (cardsInHand.Count > 0)
        {
            int targetIndex = generator.Next(cardsInHand.Count);
            chosenCard = cardsInHand[targetIndex];
        }

        return chosenCard;
    }
}
