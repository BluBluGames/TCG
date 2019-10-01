using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VisualStates
{
    Transition,
    BottomPlayerHand,
    TopPlayerHand,
    LowBoardFrontRow,
    LowBoardBackRow,
    TopBoardFrontRow,
    TopBoardBackRow,
    Dragging
}

public class WhereIsCard : MonoBehaviour
{
    private HoverPreview hover;
    private Canvas canvas;
    private int TopSortingOrder = 500;

    private int slot = -1;
    public int Slot
    {
        get { return slot; }

        set
        {
            slot = value;
        }
    }

    private VisualStates visualState;
    public VisualStates VisualState
    {
        get { return visualState; }

        set
        {
            visualState = value;
            switch (visualState)
            {
                case VisualStates.BottomPlayerHand:
                    hover.ThisPreviewEnabled = true;
                    break;
                case VisualStates.LowBoardFrontRow:
                case VisualStates.LowBoardBackRow:
                    hover.ThisPreviewEnabled = true;
                    break;
                case VisualStates.TopBoardFrontRow:
                case VisualStates.TopBoardBackRow:
                    hover.ThisPreviewEnabled = true;
                    break;
                case VisualStates.Transition:
                    hover.ThisPreviewEnabled = false;
                    break;
                case VisualStates.Dragging:
                    hover.ThisPreviewEnabled = false;
                    break;
                case VisualStates.TopPlayerHand:
                    hover.ThisPreviewEnabled = false;
                    break;
            }
        }
    }

    void Awake()
    {
        hover = GetComponent<HoverPreview>();
        if (hover == null)
            hover = GetComponentInChildren<HoverPreview>();
        canvas = GetComponentInChildren<Canvas>();
    }

    public void BringToFront()
    {
        canvas.sortingOrder = TopSortingOrder;
        canvas.sortingLayerName = "Above";
    }

    public void SetHandSortingOrder()
    {
        if (Slot != -1)
            canvas.sortingOrder = HandSortingOrder(Slot);
        canvas.sortingLayerName = "Cards";
    }

    public void SetTableSortingOrder()
    {
        canvas.sortingOrder = 0;
        canvas.sortingLayerName = "CardsOnBoard";
    }
    private int HandSortingOrder(int placeInHand)
    {
        int result = (-(placeInHand + 1) * 10);
        return result;
    }
}
