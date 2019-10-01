using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToTarget : Ability
{
    public DealDamageToTarget(PlayerController cardOwner, CardOnBoardController card, int abilityAmount, int abilityCharges) : base(cardOwner, card, abilityAmount, abilityCharges)
    { }
    public override void WhenCardPlayed()
    {
        //TODO Ability Type
        new GiveActivatedAbilityCommand(cardOwner, card, abilityCharges, abilityAmount, true).AddToQueue();
    }
}