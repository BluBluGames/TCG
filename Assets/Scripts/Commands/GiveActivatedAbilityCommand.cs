using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveActivatedAbilityCommand : Command
{
    private PlayerController CardOwner { get; }
    public CardOnBoardController CardReceivingAbility { get; }
    public int AbilityCharges { get; }
    public int AbilityAmount { get; }
    public bool IsDamaging { get; }
    private int TargetID { get; }
    private int HealingDealt { get; }

    public GiveActivatedAbilityCommand(PlayerController cardOwner, CardOnBoardController cardReceivingAbility, int abilityCharges, int abilityAmount, bool isDamaging)
    {
        CardOwner = cardOwner;
        CardReceivingAbility = cardReceivingAbility;
        AbilityCharges = abilityCharges;
        AbilityAmount = abilityAmount;
        IsDamaging = isDamaging;
    }

    public override void ExecuteCommand()
    {
        CardReceivingAbility.AbilityCharges = AbilityCharges;
        CardReceivingAbility.AbilityAmount = AbilityAmount;
        CardReceivingAbility.IsDamaging = IsDamaging;
        CommandExecutionComplete();
    }
}
