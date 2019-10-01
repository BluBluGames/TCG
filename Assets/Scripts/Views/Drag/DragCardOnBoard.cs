using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DragCardOnBoard : DraggingActions {

    private int savedHandSlot;
    private WhereIsCard whereIsCard;
    private IDHolder idScript;
    private VisualStates tempState;
    private CardView card;

    public override bool CanDrag
    {
        get
        { 
			return card.IsAllowedToBePlayed;
        }
    }

    void Awake()
    {
        whereIsCard = GetComponent<WhereIsCard>();
        card = GetComponent<CardView>();
    }

    public override void OnStartDrag()
    {
        savedHandSlot = whereIsCard.Slot;
        tempState = whereIsCard.VisualState;
        whereIsCard.VisualState = VisualStates.Dragging;
        whereIsCard.BringToFront();

    }

    public override void OnDraggingInUpdate()
    {

    }

    public override void OnEndDrag()
    {
        if (DragSuccessful() == 0)
        {
            int chosenRow = 0;
            int tablePos = playerOwner.playerView.frontRowView.BoardPositionForNewCard(Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)).x);

            playerOwner.PlayCardFromHand(GetComponent<IDHolder>().UniqueID, chosenRow, tablePos);
        }
        else if (DragSuccessful() == 1)
        {
            int chosenRow = 1;
            int tablePos = playerOwner.playerView.backRowView.TablePositionForNewCard(Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)).x);

            playerOwner.PlayCardFromHand(GetComponent<IDHolder>().UniqueID, chosenRow, tablePos);
        }
        else
        {
            // Set old sorting order 
            whereIsCard.SetHandSortingOrder();
            whereIsCard.VisualState = tempState;
            // Move this card back to its slot position
            HandView PlayerHand = playerOwner.playerView.handView;
            Vector3 oldCardPos = PlayerHand.slots.Children[savedHandSlot].transform.localPosition;
            transform.DOLocalMove(oldCardPos, 1f);
        } 
    }

    protected override int DragSuccessful()
    {
        bool TableNotFull = (playerOwner.frontRow.CardsOnFrontRow.Count < 8);

        if (FrontRowView.CursorOverSomeTable && TableNotFull)
        {
            return 0;
        }
        else if (BackRowView.CursorOverSomeTable && TableNotFull)
        {
            return 1;
        }
        return -1;
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
