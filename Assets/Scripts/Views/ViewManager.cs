using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    [Header("Players")]
    public PlayerController TopPlayer;
    public PlayerController BottomPlayer;
    [Header("Prefabs and Assets")]
    public GameObject cardInHandPrefab;
    public GameObject cardOnBoardPrefab;
    [Header("Other")]
    public Button EndTurnButton;
    public Text PlayerHealth;
    public Text EnemyHealth;
    public GameObject GameOverPanel;
    public GameObject YouWonInfo;
    public GameObject YouLostInfo;

    public Dictionary<AreaPosition, PlayerController> Players = new Dictionary<AreaPosition, PlayerController>();

    private static ViewManager _instance;
    public static ViewManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("View manager is null");
            }
            return _instance;
        }

    }

    /// <summary>
    /// Assigns Views to Player Controllers and instantiate singleton
    /// </summary>
    private void Awake()
    {
        Players.Add(AreaPosition.Top, TopPlayer);
        Players.Add(AreaPosition.Bottom, BottomPlayer);
        _instance = this;
    }

    public bool CanControlThisPlayer(AreaPosition areaViewOwner)
    {
        bool PlayersTurn = (GameManager.Instance.WhoseTurn == Players[areaViewOwner]);
        return Players[areaViewOwner].playerView.AllowedToControlThisPlayer && Players[areaViewOwner].playerView.ControlsON && PlayersTurn;
    }

    public bool CanControlThisPlayer(PlayerController ownerPlayer)
    {
        bool PlayersTurn = (GameManager.Instance.WhoseTurn == ownerPlayer);
        return ownerPlayer.playerView.AllowedToControlThisPlayer && ownerPlayer.playerView.ControlsON && PlayersTurn;
    }

    public void EnableEndTurnButtonOnStart(PlayerController ownerPlayer)
    {
        if (ownerPlayer == BottomPlayer && CanControlThisPlayer(AreaPosition.Bottom) || ownerPlayer == TopPlayer && CanControlThisPlayer(AreaPosition.Top))
            EndTurnButton.interactable = true;
        else
            EndTurnButton.interactable = false;
    }
}
