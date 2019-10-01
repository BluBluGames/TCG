using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all cards in players hand as a List
/// </summary>
public class HandController : MonoBehaviour
{
    ///<value>holds number of cards player can have in his hand</value>
    public readonly int handLimit = 7;
    ///<value>Holds all cards in hand as List</value>
    public List<CardController> CardsInHand = new List<CardController>();
}
