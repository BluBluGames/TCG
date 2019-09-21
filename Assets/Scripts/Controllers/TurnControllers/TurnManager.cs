using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnManager : MonoBehaviour
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
