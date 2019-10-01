using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Core class of the game - this is the starting point of every game and it manages flow of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game manager is null");
            }
            return _instance;
        }

    }

    private void Awake()
    {
        _instance = this;
    }

    private readonly int cardsDrawnOnGameStart = 7;

    private PlayerController whoseTurn;

    private static bool isHeadlessMode;
    public static bool IsHeadlessMode { get => isHeadlessMode; set => isHeadlessMode = value; }

    public PlayerController WhoseTurn
    {
        get { return whoseTurn; }
        set
        {
            StartTurn(value);
        }
    }

    void Start()
    {
        OnGameStart();
    }

    /// <summary>
    /// On start of the game
    /// clear lists of cards to be sure there are no leftovers
    /// choose who goes first randomly
    /// draw cards for both players
    /// add visual elements to the game
    /// execute start turn command
    /// </summary>
    public void OnGameStart()
    {
        CardController.CardsCreatedThisGame.Clear();
        CardOnBoardController.CardsPlayedThisGame.Clear();

        PlayerController whoIsFirst = PlayerController.Players[UnityEngine.Random.Range(0, 2)];
        PlayerController whoIsSecond = whoIsFirst.otherPlayer;

        for (int i = 0; i < cardsDrawnOnGameStart; i++)
        {
            whoIsFirst.DrawCard();
            whoIsSecond.DrawCard();
        }

        foreach (PlayerController player in PlayerController.Players)
        {

        }

        new StartTurnCommand(whoIsFirst).AddToQueue();
    }

    private void StartTurn(PlayerController player)
    {
        if (player.hand.CardsInHand.Count > 0)
        {
            whoseTurn = player;
            TurnController turnController = WhoseTurn.GetComponent<TurnController>();
            turnController.OnTurnStart();
        }
        else
            player.CheckIfGameEnded();
    }
    public void EndTurn()
    {
        bool isGameEnded = WhoseTurn.CheckIfGameEnded();
        if (!isGameEnded)
        {
            TurnController turnController = WhoseTurn.GetComponent<TurnController>();
            new StartTurnCommand(WhoseTurn.otherPlayer).AddToQueue();
        }
    }
}
