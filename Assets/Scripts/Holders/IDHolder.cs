using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Holds all IDs of cards in game. IDs are used in targetting system
/// </summary>
public class IDHolder : MonoBehaviour
{
    public int UniqueID;
    private static List<IDHolder> allIDHolders = new List<IDHolder>();

    /// <summary>
    /// Adds unique Id to object on creation
    /// </summary>
    void Awake()
    {
        allIDHolders.Add(this);
    }

    /// <summary>
    /// Gets object in game
    /// </summary>
    /// <param name="ID">Int - Unique Id of card</param>
    /// <returns></returns>
    public static GameObject GetGameObjectWithID(int ID)
    {
        foreach (IDHolder i in allIDHolders)
        {
            if (i.UniqueID == ID)
                return i.gameObject;
        }
        return null;
    }

    /// <summary>
    /// Clears whole list of IDs - if there are any objects still present in game they wont be targetable
    /// </summary>
    public static void ClearIDHoldersList()
    {
        allIDHolders.Clear();
    }
}
