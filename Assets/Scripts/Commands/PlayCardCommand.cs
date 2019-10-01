using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class used to place cards on board - executes inserting card on the board and deleting it from hand
/// </summary>
public class PlayCardCommand : Command
{
    private CardController Card;
    private PlayerController Player;
    private int RowPosition;
    private int ChosenRow;

    public PlayCardCommand(CardController card, PlayerController player, int chosenRow, int rowPosition)
    {
        Card = card ?? throw new ArgumentNullException(nameof(card));
        Player = player ?? throw new ArgumentNullException(nameof(player));
        RowPosition = rowPosition;
        ChosenRow = chosenRow;
    }

    public override void ExecuteCommand()
    {
        CardOnBoardController newCardOnBoard = new CardOnBoardController(Player, Card.cardAsset);
        Player.PlayerHealth += newCardOnBoard.cardAsset.cardHealth;

        if (ChosenRow == 0)
            Player.frontRow.CardsOnFrontRow.Insert(RowPosition, newCardOnBoard);
        else
            Player.backRow.CardsOnBackRow.Insert(RowPosition, newCardOnBoard);

        if (newCardOnBoard.abilityToBeExecuted != null)
            newCardOnBoard.abilityToBeExecuted.WhenCardPlayed();

        Player.hand.CardsInHand.Remove(Card);

        if (GameManager.IsHeadlessMode == false)
        {
            HandView PlayerHand = Player.playerView.handView;
            GameObject card = IDHolder.GetGameObjectWithID(Card.ID);
            PlayerHand.RemoveCard(card);
            HoverPreview.PreviewsAllowed = true;
            if (ChosenRow == 0)
                Player.playerView.frontRowView.AddCardAtIndex(Card.cardAsset, newCardOnBoard.ID, RowPosition);
            else
                Player.playerView.backRowView.AddCardAtIndex(Card.cardAsset, newCardOnBoard.ID, RowPosition);

            newCardOnBoard.cardOwner.playerView.playerHealth.playerHealth.text = newCardOnBoard.cardOwner.PlayerHealth.ToString();

            new DestroyObjectCommand(card).AddToQueue();
        }
        CommandExecutionComplete();
    }
}
