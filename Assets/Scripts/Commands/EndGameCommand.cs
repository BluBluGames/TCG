using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ends the game and calculates points
/// </summary>
public class EndGameCommand : Command
{
    ///<value>Current player</value>
    private PlayerController player;
    bool isWinner;
    bool isDraw = false;

    /// <summary>
    /// Ends the game
    /// </summary>
    /// <param name="player"></param>
    public EndGameCommand(PlayerController player)
    {
        this.player = player;
    }

    /// <summary>
    /// Calculates points and determines who won
    /// </summary>
    public override void ExecuteCommand()
    {
        int thisPlayersHealth = player.PlayerHealth;
        int otherPlayersHealth = player.otherPlayer.PlayerHealth;

        if (thisPlayersHealth > otherPlayersHealth)
            isWinner = true;
        else if (thisPlayersHealth == otherPlayersHealth)
            isDraw = true;
        else
            isWinner = false;

        Time.timeScale = 0;

        if (GameManager.IsHeadlessMode == false)
        {
            ViewManager.Instance.GameOverPanel.SetActive(true);
            if (isWinner)
            {
                ViewManager.Instance.YouLostInfo.SetActive(false);
                ViewManager.Instance.YouWonInfo.SetActive(true);
            }
            else if (isWinner == false)
            {
                ViewManager.Instance.YouWonInfo.SetActive(true);
                ViewManager.Instance.YouLostInfo.SetActive(true);
            }
            else if (isDraw)
            {
                ViewManager.Instance.YouWonInfo.SetActive(false);
                ViewManager.Instance.YouLostInfo.SetActive(true);
            }
        }

        CommandExecutionComplete();
    }
}
