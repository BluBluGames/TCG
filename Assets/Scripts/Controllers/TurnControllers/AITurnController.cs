using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles playing cards by AI
/// </summary>
public class AITurnController : TurnController
{
    /// <summary>
    /// Entry point for turn execution - leads to PlayerController OnStartTUrn()
    /// </summary>
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        ExecuteAITurn();
        GameManager.Instance.EndTurn();
    }

    /// <summary>
    /// Executes AITurn
    /// </summary>
    private void ExecuteAITurn()
    {
        PlayCardFromHand();
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
            player.PlayCardFromHand(chosenCard, row, 0);
        }
    }

    /// <summary>
    /// Chooses between values 0 and 1 - 0 is Front Row, 1 is Back Row
    /// </summary>
    /// <returns></returns>
    private int ChooseRandomRowToPlayCardTo()
    {
        System.Random random = new System.Random();
        int row = random.Next(0, 2);
        return row;
    }

    /// <summary>
    /// Chooses Random card from hand
    /// </summary>
    /// <returns></returns>
    private CardController ChooseRandomCardFromHand()
    {
        System.Random generator = new System.Random();
        List<CardController> cardsInHand = GameManager.Instance.WhoseTurn.hand.CardsInHand;

        CardController chosenCard = null;
        if (cardsInHand.Count > 0)
        {
            int targetIndex = generator.Next(cardsInHand.Count);
            chosenCard = cardsInHand[targetIndex];
        }

        return chosenCard;
    }
}
