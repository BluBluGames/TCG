using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all cards in players hand as a List
/// </summary>
public class HandController : MonoBehaviour
{
    ///<value>Holds all cards in hand as List</value>
    public List<CardController> CardsInHand = new List<CardController>();
}
