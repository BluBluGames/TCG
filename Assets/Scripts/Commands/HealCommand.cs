using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCommand : Command
{
    private PlayerController CardOwner { get; }
    private int TargetID { get; }
    private int HealingDealt { get; }

    public HealCommand(PlayerController cardOwner, int targetID, int healingDealt)
    {
        CardOwner = cardOwner;
        TargetID = targetID;
        HealingDealt = healingDealt;
    }

    public override void ExecuteCommand()
    {
        GameObject target = IDHolder.GetGameObjectWithID(TargetID);
        CardOnBoardController targetCard = CardOnBoardController.CardsPlayedThisGame[TargetID];

        targetCard.CardHealth += HealingDealt;
        targetCard.cardOwner.PlayerHealth += HealingDealt;

        if (GameManager.IsHeadlessMode == false)
        {
            target.GetComponent<CardOnBoardView>().healthText.text = targetCard.CardHealth.ToString();
            targetCard.cardOwner.playerView.playerHealth.playerHealth.text = targetCard.cardOwner.PlayerHealth.ToString();
        }

        CommandExecutionComplete();
    }
}
