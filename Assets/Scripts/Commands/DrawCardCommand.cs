using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draw a card only if deck is not empty and cards in hand arent at maximum capacity
/// </summary>
public class DrawCardCommand : Command
{
    private PlayerController Player;
    private CardController cardDrawn;

    public DrawCardCommand(PlayerController player)
    {
        Player = player ?? throw new ArgumentNullException(nameof(player));
    }

    public override void ExecuteCommand()
    {
        if (Player.deck.cardsInDeck.Count > 0)
        {
            if (Player.hand.CardsInHand.Count <= Player.hand.handLimit)
            {
                CardController newCard = new CardController(Player.deck.cardsInDeck[0]);
                newCard.playerHoldingCard = Player;
                Player.hand.CardsInHand.Insert(0, newCard);
                Player.deck.cardsInDeck.RemoveAt(0);

                if (GameManager.IsHeadlessMode == false)
                {
                    Player.playerView.handView.GivePlayerACard(newCard.cardAsset, newCard.ID);
                }
            }
        }

        CommandExecutionComplete();

    }
}
