using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageCommand : Command
{
    private PlayerController OwnerOfCardBeingDamaged { get; }
    private int TargetID { get; }
    private int DamageDealt { get; }

    public DealDamageCommand(PlayerController ownerOfCardBeingDamaged, int targetID, int damageDealt)
    {
        OwnerOfCardBeingDamaged = ownerOfCardBeingDamaged;
        TargetID = targetID;
        DamageDealt = damageDealt;
    }

    public override void ExecuteCommand()
    {
        GameObject target = IDHolder.GetGameObjectWithID(TargetID);
        target.GetComponent<CardOnBoardController>().TakeDamage(DamageDealt);
        //OwnerOfCardBeingDamaged;

    }
}
