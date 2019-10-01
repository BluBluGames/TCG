using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds cards placed on front row as a list
/// </summary>
public class FrontRowController : MonoBehaviour
{
    ///<value>holds number of cards player can have in this row</value>
    public readonly int rowLimit = 7;

    public List<CardOnBoardController> CardsOnFrontRow = new List<CardOnBoardController>();

    /// <summary>
    /// places card on back row
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cardOnFrontRow"></param>
    public void PlaceCardAt(int index, CardOnBoardController cardOnFrontRow)
    {
        CardsOnFrontRow.Insert(index, cardOnFrontRow);
    }

}
