using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackRowView : MonoBehaviour
{
    public AreaPosition owner;
    public SameDistanceChildren slots;
    private List<GameObject> cardsInRow = new List<GameObject>();
    private bool cursorOverThisRow = false;
    private BoxCollider col;


    public static bool CursorOverSomeTable
    {
        get
        {
            BackRowView[] bothTables = FindObjectsOfType<BackRowView>();
            return (bothTables[0].CursorOverThisRow || bothTables[1].CursorOverThisRow);
        }
    }

    public bool CursorOverThisRow
    {
        get{ return cursorOverThisRow; }
    }

    void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 50f);

        bool passedThroughTableCollider = false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider == col)
                passedThroughTableCollider = true;
        }
        cursorOverThisRow = passedThroughTableCollider;
    }
   
    public void AddCardAtIndex(CardAsset cardAsset, int UniqueID ,int index)
    {
        GameObject card = Instantiate(ViewManager.Instance.cardOnBoardPrefab, slots.Children[index].transform.position, Quaternion.identity) as GameObject;

        CardOnBoardView cardView = card.GetComponent<CardOnBoardView>();
        cardView.cardAsset = cardAsset;
        cardView.displayCardOnBoardVisual();

        foreach (Transform transform in card.GetComponentsInChildren<Transform>())
            transform.tag = owner.ToString()+ "CardOnBackRow";
        
        card.transform.SetParent(slots.transform);

        cardsInRow.Insert(index, card);

        WhereIsCard whereIsCard = card.GetComponent<WhereIsCard>();
        whereIsCard.Slot = index;
        whereIsCard.VisualState = VisualStates.LowBoardBackRow;

        IDHolder id = card.AddComponent<IDHolder>();
        id.UniqueID = UniqueID;

        ShiftSlotsGameObjectAccordingToNumberOfCardsOnBoard();
        PlaceCardsOnNewSlots();

        Command.CommandExecutionComplete();
    }

    public int TablePositionForNewCard(float MouseX)
    {
        if (cardsInRow.Count == 0 || MouseX > slots.Children[0].transform.position.x)
            return 0;
        else if (MouseX < slots.Children[cardsInRow.Count - 1].transform.position.x)
            return cardsInRow.Count;
        for (int i = 0; i < cardsInRow.Count; i++)
        {
            if (MouseX < slots.Children[i].transform.position.x && MouseX > slots.Children[i + 1].transform.position.x)
                return i + 1;
        }
        return 0;
    }

    public void RemoveCardWithID(int IDToRemove)
    {
        GameObject cardToRemove = IDHolder.GetGameObjectWithID(IDToRemove);
        cardsInRow.Remove(cardToRemove);
        Destroy(cardToRemove);

        ShiftSlotsGameObjectAccordingToNumberOfCardsOnBoard();
        PlaceCardsOnNewSlots();
        Command.CommandExecutionComplete();
    }

    void ShiftSlotsGameObjectAccordingToNumberOfCardsOnBoard()
    {
        float posX;
        if (cardsInRow.Count > 0)
            posX = (slots.Children[0].transform.localPosition.x - slots.Children[cardsInRow.Count - 1].transform.localPosition.x) / 2f;
        else
            posX = 0f;

        slots.gameObject.transform.DOLocalMoveX(posX, 0.3f);  
    }

    void PlaceCardsOnNewSlots()
    {
        foreach (GameObject g in cardsInRow)
        {
            if (g != null)
            {
                g.transform.DOLocalMoveX(slots.Children[cardsInRow.IndexOf(g)].transform.localPosition.x, 0.3f);
            }
        }
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
