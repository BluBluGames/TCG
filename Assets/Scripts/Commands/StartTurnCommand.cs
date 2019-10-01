using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Starts current players turn
/// </summary>
public class StartTurnCommand : Command
{
    ///<value>Current player</value>
    private PlayerController player;

    /// <summary>
    /// Starts turn of player passed in the parameter
    /// </summary>
    /// <param name="player"></param>
    public StartTurnCommand(PlayerController player)
    {
        this.player = player;
    }

    /// <summary>
    /// Sets information on who currently has his turn
    /// </summary>
    public override void ExecuteCommand()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
        GameManager.Instance.WhoseTurn = player;
        CommandExecutionComplete();
    }
}
