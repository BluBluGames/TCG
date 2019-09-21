using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Creates an ID
/// </summary>
public static class IDCreator
{
    ///<value>value of ID that is to be assigned</value>
    private static int ID;

    /// <summary>
    /// Creates unique IDs - to ensure they are unique value of previous ID is stored in ID variable and incremented each time this method gets called
    /// </summary>
    /// <returns></returns>
    public static int CreateUniqueID()
    {
        ID++;
        return ID;
    }

    /// <summary>
    /// Resets value of previous ID to 0 - Should be used at End and Start of game only
    /// </summary>
    public static void ResetIDs()
    {
        ID = 0;
    }


}
