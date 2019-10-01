using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Blueprint for creating card
/// </summary>
[CreateAssetMenu(menuName = "Cards/Card")]
public class CardAsset : ScriptableObject
{
    [Header("Core Info")]
    ///<value>faction this card belongs to</value>
    public Faction faction;
    public string cardName;
    public Sprite cardArt;
    public Sprite cardFrame;
    public Sprite cardIcon;
    ///<value>information about if this card should be placed on board</value>
    public bool isPersistent;
    ///<value>information if this is a creature card</value>
    public bool isCreature;

    [Header("Details")]
    ///<value>health card enters the game with</value>
    public int cardHealth;

    [Header("Ability Types - Choose One")]
    //TODO this could be changed to list of bools
    ///<value>String with script name used to execute ability</value>
    public string cardAbility;

    [Header("Ability Details")]
    ///<value>Holds amount of effect ability does - for example damage</value>
    public int abilityAmount;
    public int abilityCharges;
}
