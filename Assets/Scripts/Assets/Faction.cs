using UnityEngine;
using System.Collections;

/// <summary>
/// Current factions available in game
/// </summary>
public enum Factions { Nilfgaard, NorthernKingdoms, Monsters, ScoiaTael }

/// <summary>
/// Blueprint for creating new factions in the game
/// </summary>
[CreateAssetMenu(menuName = "Factions/Faction")]
public class Faction : ScriptableObject
{
    public string factionName;
    public Sprite icon;
}
