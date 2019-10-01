using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageCommand : Command
{
    private PlayerController OwnerOfCardBeingDamaged { get; }
    private int TargetID { get; }
    private int DamageDealt { get; }

    public DealDamageCommand(PlayerController cardOwner, int targetID, int damageDealt)
    {
        OwnerOfCardBeingDamaged = cardOwner;
        TargetID = targetID;
        DamageDealt = damageDealt;
    }

    public override void ExecuteCommand()
    {
        GameObject target = IDHolder.GetGameObjectWithID(TargetID);
        CardOnBoardController targetCard = CardOnBoardController.CardsPlayedThisGame[TargetID];

        int currentTargetedCardHealth = targetCard.CardHealth;

        if (currentTargetedCardHealth > DamageDealt)
        {
            targetCard.CardHealth -= DamageDealt;
            targetCard.cardOwner.PlayerHealth -= DamageDealt;
        }
        else
        {
            int healthToSubtract = targetCard.CardHealth;
            targetCard.CardHealth -= healthToSubtract;
            targetCard.cardOwner.PlayerHealth -= healthToSubtract;
        }

        if (targetCard.CardHealth<=0)
        {
            new DestroyObjectCommand(target).AddToQueue();
        }

        if (GameManager.IsHeadlessMode == false)
        {
            if (GameManager.IsHeadlessMode == false)
            {
                target.GetComponent<CardOnBoardView>().healthText.text = targetCard.CardHealth.ToString();
                targetCard.cardOwner.playerView.playerHealth.playerHealth.text = targetCard.cardOwner.PlayerHealth.ToString();
            }
        }

        CommandExecutionComplete();
    }
}
