using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds cards placed on back row as a list
/// </summary>
public class BackRowController : MonoBehaviour
{
    ///<value>holds number of cards player can have in this row</value>
    public readonly int rowLimit = 7;

    public List<CardOnBoardController> CardsOnBackRow = new List<CardOnBoardController>();

    /// <summary>
    /// places card on back row
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cardOnBackRow"></param>
    public void PlaceCardat(int index, CardOnBoardController cardOnBackRow)
    {
        CardsOnBackRow.Insert(index, cardOnBackRow);
    }

}
