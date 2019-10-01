using UnityEngine;
using System.Collections;

public abstract class DraggingActions : MonoBehaviour {

    public abstract void OnStartDrag();

    public abstract void OnEndDrag();

    public abstract void OnDraggingInUpdate();

    public virtual bool CanDrag
    {
        get
        {            
            return ViewManager.Instance.CanControlThisPlayer(playerOwner);
        }
    }

    protected virtual PlayerController playerOwner
    {
        get{
            
            if (tag.Contains("Bottom"))
                return ViewManager.Instance.BottomPlayer;
            else if (tag.Contains("Top"))
                return ViewManager.Instance.TopPlayer;
            else
            {
                Debug.LogError("Untagged Card " + transform.parent.name);
                return null;
            }
        }
    }

    protected abstract int DragSuccessful();
}
