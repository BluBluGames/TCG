using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Draggable : MonoBehaviour
{ 
    private bool dragging = false;
    private Vector3 pointerDisplacement;
    private float zDisplacement;
    private DraggingActions dragAction;

    private static Draggable _draggingThis;
    public static Draggable DraggingThis
    {
        get{ return _draggingThis;}
    }

    void Awake()
    {
        dragAction = GetComponent<DraggingActions>();
    }

    void OnMouseDown()
    {
        if (dragAction!=null)
        {
            dragging = true;
            HoverPreview.PreviewsAllowed = false;
            _draggingThis = this;
            dragAction.OnStartDrag();
            zDisplacement = -Camera.main.transform.position.z + transform.position.z;
            pointerDisplacement = -transform.position + MouseInWorldCoords();
        }
    }

    void Update ()
    {
        if (dragging)
        { 
            Vector3 mousePos = MouseInWorldCoords();
            transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);   
            dragAction.OnDraggingInUpdate();
        }
    }
	
    void OnMouseUp()
    {
        if (dragging)
        {
            dragging = false;
            HoverPreview.PreviewsAllowed = true;
            _draggingThis = null;
            dragAction.OnEndDrag();
        }
    }   

    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
