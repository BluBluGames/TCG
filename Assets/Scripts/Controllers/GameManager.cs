using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private PlayerController _whoseTurn;
    public PlayerController whoseTurn
    {
        get { return _whoseTurn; }
        set { _whoseTurn = value; }
    }

    public void EndTurn()
    {

    }
}
