using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardOnBoardController
{
    ///<value>Getter for this card on board ID</value>
    public PlayerController cardOwner;

    ///<value>Holds information about this card on board taken from the card asset</value>
    public CardAsset cardAsset;

    ///<value>holds information about ability to be executed on given circumenstances</value>
    public Ability abilityToBeExecuted;

    ///<value>Card on board ID - used for targetting</value>
    private int CardOnBoardID;

    ///<value>Ensures that abilities can be used once per turn</value>
    public bool isAbilityUsedThisTurn;

    ///<value>Getter for this card on board ID</value>
    public int ID
    {
        get { return CardOnBoardID; }
    }

    ///<value>holds current card health</value>
    private int cardHealth;
    ///<value>Getter and Setter for this card on board current health</value>
    public int CardHealth
    {
        get { return cardHealth; }

        set
        {
                cardHealth = value;
        }
    }

    private int abilityCharges;
    ///<value>Gets and Sets number of ability charges this card has</value>
    public int AbilityCharges { get => abilityCharges; set => abilityCharges = value; }

    private int abilityAmount;
    ///<value>Gets and Sets number of ability strength this card has</value>
    public int AbilityAmount { get => abilityAmount; set => abilityAmount = value; }

    private bool isDamaging;
    public bool IsDamaging { get => isDamaging; set => isDamaging = value; }
    ///<value>Returns true if its this card owners turn, card has charges, and didnt use ability this turn</value>
    public bool CanUseAbility
    {
        get
        {
            bool ownersTurn = (GameManager.Instance.WhoseTurn == cardOwner);
            return (ownersTurn && (AbilityCharges > 0) && !isAbilityUsedThisTurn);
        }
    }

    ///<value>Disctionary holds all cards played during this game based on their ID</value>
    public static Dictionary<int, CardOnBoardController> CardsPlayedThisGame = new Dictionary<int, CardOnBoardController>();


    /// <param name="cardOwner">current player</param>
    /// <param name="cardAsset">chosen card to be played</param>
    public CardOnBoardController(PlayerController cardOwner, CardAsset cardAsset)
    {
        this.cardOwner = cardOwner ?? throw new ArgumentNullException(nameof(cardOwner));
        this.cardAsset = cardAsset ?? throw new ArgumentNullException(nameof(cardAsset));
        RegisterCardAbility(cardOwner, cardAsset);
        CardHealth = cardAsset.cardHealth;
        CardOnBoardID = IDCreator.CreateUniqueID();
        CardsPlayedThisGame.Add(CardOnBoardID, this);
    }

    /// <summary>
    /// This cards operations on start of turn
    /// </summary>
    public void OnTurnStart()
    {
        isAbilityUsedThisTurn = false;
    }

    /// <summary>
    /// Checks if there is any ability registered on card asset and registers an event to this ability
    /// </summary>
    /// <param name="cardOwner">this cards owner</param>
    /// <param name="cardAsset">base card values</param>
    private void RegisterCardAbility(PlayerController cardOwner, CardAsset cardAsset)
    {
        if (cardAsset.cardAbility != null && cardAsset.cardAbility != "")
        {
            abilityToBeExecuted = System.Activator.CreateInstance(
                Type.GetType(cardAsset.cardAbility), new object[] { cardOwner, this, cardAsset.abilityAmount, cardAsset.abilityCharges }
                ) as Ability;
            abilityToBeExecuted.RegisterEventEffect();
        }
    }

    public void UseAbilityOnTargetID(int cardOnBoardID)
    {
        if (CanUseAbility && !isAbilityUsedThisTurn)
        {
            CardOnBoardController target = CardsPlayedThisGame[cardOnBoardID];
            UseAbilityOnCard(target);
            isAbilityUsedThisTurn = true;
            AbilityCharges--;
        }
    }

    /// <summary>
    /// Uses ability on card
    /// </summary>
    /// <param name="target">target of this ability</param>
    /// <param name="abilityAmount">Number of poits ability will heal or damage</param>
    /// <param name="abilityType">determines if this ability will heal or deal damage to target</param>
    public void UseAbilityOnCard(CardOnBoardController target)
    {
        if (CanUseAbility && !isAbilityUsedThisTurn)
        {
            //TODO Use other types of abilites
            if (isDamaging)
                new DealDamageCommand(cardOwner, target.ID, AbilityAmount).AddToQueue();
            else
            {
                new HealCommand(cardOwner, target.ID, AbilityAmount).AddToQueue();
            }
            isAbilityUsedThisTurn = true;
        }
    }
}
