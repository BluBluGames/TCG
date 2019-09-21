using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds cards placed on back row as a list
/// </summary>
public class BackRowController : MonoBehaviour
{
    public List<CardOnBoardController> CardsOnBackRow = new List<CardOnBoardController>();

    /// <summary>
    /// places card on back row
    /// </summary>
    /// <param name="index"></param>
    /// <param name="cardOnBackRow"></param>
    public void PlaceCreatureAt(int index, CardOnBoardController cardOnBackRow)
    {
        CardsOnBackRow.Insert(index, cardOnBackRow);
    }

}
