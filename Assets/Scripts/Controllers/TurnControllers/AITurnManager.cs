using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurnManager : TurnManager
{
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        ExecuteAITurn();
    }

    private void ExecuteAITurn()
    {
        PlayCardFromHand();
        GameManager.Instance.EndTurn();
    }

    private void PlayCardFromHand()
    {
        var generator = new System.Random();
        List<CardController> cardsInHand = GameManager.Instance.whoseTurn.hand.CardsInHand;

        if (cardsInHand.Count > 0)
        {
            int targetIndex = generator.Next(cardsInHand.Count);
            CardController chosenCard = cardsInHand[targetIndex];

            player.PlayCardFromHand(chosenCard, 0);
        }
    }
}
