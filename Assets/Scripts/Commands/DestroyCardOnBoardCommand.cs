using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCardOnBoardCommand : Command
{
    public int CardID { get; }
    public PlayerController Player { get; }

    public DestroyCardOnBoardCommand(int CardID, PlayerController player)
    {
        this.CardID = CardID;
        Player = player;
    }

    public override void ExecuteCommand()
    {
        Debug.Log("Card destroyed");
    }
}
