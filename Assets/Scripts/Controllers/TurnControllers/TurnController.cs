using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class for turn controllers
/// </summary>
public abstract class TurnController : MonoBehaviour
{
    protected PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    public  virtual void OnTurnStart()
    {
        player.OnTurnStart();
    }
}
