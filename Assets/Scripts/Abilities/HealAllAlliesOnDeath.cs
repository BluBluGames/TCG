using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HealAllAlliesOnDeath : Ability
{
    public HealAllAlliesOnDeath(PlayerController cardOwner, CardOnBoardController card, int abilityAmount, int abilityCharges) : base(cardOwner, card, abilityAmount, abilityCharges) { }

    public override void WhenCardIsDestroyed()
    {
        //TODO Assume that player can kill his own minion in check
        List<CardOnBoardController> cardsOnFrontRow = GameManager.Instance.WhoseTurn.otherPlayer.frontRow.CardsOnFrontRow;
        List<CardOnBoardController> cardsOnBackRow = GameManager.Instance.WhoseTurn.otherPlayer.backRow.CardsOnBackRow;

        List<CardOnBoardController> allCards = cardsOnFrontRow.Concat(cardsOnBackRow).ToList();

        foreach (CardOnBoardController card in allCards)
        {
            new HealCommand(cardOwner, card.ID, abilityAmount).AddToQueue();
        }
    }
}
