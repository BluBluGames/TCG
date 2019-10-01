using UnityEngine;
using System.Collections;

///<summary>
///Distinguishes betwwen area belonging to bottom and top player
///</summary>
public enum AreaPosition { Top, Bottom }

public class PlayerView : MonoBehaviour
{
    public AreaPosition owner;
    public bool ControlsON = true;
    public HandView handView;
    public PlayerInfoView playerInfoView;
    public PlayerHealthView playerHealth;
    public FrontRowView frontRowView;
    public BackRowView backRowView;

    public bool AllowedToControlThisPlayer
    {
        get;
        set;
    }
}
