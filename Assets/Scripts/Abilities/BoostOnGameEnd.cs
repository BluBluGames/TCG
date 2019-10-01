using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostOnGameEnd : Ability
{
    public BoostOnGameEnd(PlayerController cardOwner, CardOnBoardController card, int abilityAmount, int abilityCharges) : base(cardOwner, card, abilityAmount, abilityCharges) { }

    public override void CauseEventEffect()
    {
        new HealCommand(cardOwner, card.ID, abilityAmount).AddToQueue();
    }

    public override void RegisterEventEffect()
    {
        cardOwner.OnGameEnd += CauseEventEffect;
    }
}
