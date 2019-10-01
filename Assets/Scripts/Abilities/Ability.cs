using UnityEngine;
using System.Collections;

public abstract class Ability
{
    protected PlayerController cardOwner;
    protected CardOnBoardController card;
    protected int abilityAmount;
    protected int abilityCharges;

    public Ability(PlayerController cardOwner, CardOnBoardController card, int abilityAmount, int abilityCharges)
    {
        this.cardOwner = cardOwner;
        this.card = card;
        this.abilityAmount = abilityAmount;
        this.abilityCharges = abilityCharges;
    }

    public virtual void RegisterEventEffect() { }

    public virtual void UnRegisterEventEffect() { }

    public virtual void CauseEventEffect() { }

    public virtual void WhenCardPlayed() { }

    public virtual void WhenCardIsDestroyed() { }
}
