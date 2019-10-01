using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command destroys passed game object - added to queue to ensure no ether effects after this execution could reach that object
/// </summary>
public class DestroyObjectCommand : Command
{
    public UnityEngine.Object ObjectToDestroy { get; }

    public DestroyObjectCommand(UnityEngine.Object objectToDestroy)
    {
        ObjectToDestroy = objectToDestroy;
    }

    public override void ExecuteCommand()
    {
        UnityEngine.Object.Destroy(ObjectToDestroy);
        CommandExecutionComplete();       
    }
}
