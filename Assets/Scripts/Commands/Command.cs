using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Command base class - implements Queue of commands to ensure no methods will be called before previous command and associated views are finished
/// </summary>
public class Command
{
    public static Queue<Command> CommandQueue = new Queue<Command>();
    public static bool executingQueue = false;

    /// <summary>
    /// Adds item to queue - will be executed after all previously added elements were
    /// </summary>
    public virtual void AddToQueue()
    {
        CommandQueue.Enqueue(this);
        if (!executingQueue)
            ExecuteFirstCommandFromQueue();
    }

    /// <summary>
    /// Used to list all steps that need to be done in current command - every command should implement this
    /// </summary>
    public virtual void ExecuteCommand()
    {

    }

    /// <summary>
    /// Call this at the end of every derived command. It fires another command in queue or sets information that queue is empty.
    /// </summary>
    public static void CommandExecutionComplete()
    {
        if (CommandQueue.Count > 0)
            ExecuteFirstCommandFromQueue();
        else
            executingQueue = false;
    }

    /// <summary>
    /// Method executes first command from queue 
    /// </summary>
    public static void ExecuteFirstCommandFromQueue()
    {
        executingQueue = true;
        CommandQueue.Dequeue().ExecuteCommand();
    }

}
