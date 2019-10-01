using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandView : MonoBehaviour
{
    public AreaPosition owner;
    public SameDistanceChildren slots;

    public bool TakeCardsOpenly = true;

    private List<GameObject> cardsInHand = new List<GameObject>();

    public void AddCard(GameObject card)
    {
        cardsInHand.Insert(0, card);

        card.transform.SetParent(slots.transform);

        PlaceCardsOnNewSlots();
        UpdatePlacementOfSlots();
    }

    public void RemoveCard(GameObject card)
    {
        cardsInHand.Remove(card);

        PlaceCardsOnNewSlots();
        UpdatePlacementOfSlots();
    }

    public void RemoveCardAtIndex(int index)
    {
        cardsInHand.RemoveAt(index);
        PlaceCardsOnNewSlots();
        UpdatePlacementOfSlots();
    }

    public GameObject GetCardAtIndex(int index)
    {
        return cardsInHand[index];
    }

    void UpdatePlacementOfSlots()
    {
        float posX;
        if (cardsInHand.Count > 0)
            posX = (slots.Children[0].transform.localPosition.x - slots.Children[cardsInHand.Count - 1].transform.localPosition.x) / 2f;
        else
            posX = 0f;

        slots.gameObject.transform.DOLocalMoveX(posX, 0.3f);
    }

    void PlaceCardsOnNewSlots()
    {
        foreach (GameObject gameObject in cardsInHand)
        {
            gameObject.transform.DOLocalMoveX(slots.Children[cardsInHand.IndexOf(gameObject)].transform.localPosition.x, 0.3f);

            WhereIsCard whereIsCard = gameObject.GetComponent<WhereIsCard>();
            whereIsCard.Slot = cardsInHand.IndexOf(gameObject);
            whereIsCard.SetHandSortingOrder();
        }
    }

    GameObject CreateACardAtPosition(CardAsset cardAsset, Vector3 position)
    {
        GameObject card;

        card = Instantiate(ViewManager.Instance.cardInHandPrefab, position, Quaternion.identity) as GameObject;

        CardView cardView = card.GetComponent<CardView>();
        cardView.cardAsset = cardAsset;
        cardView.displayCardView();

        return card;
    }

    public void GivePlayerACard(CardAsset cardAsset, int UniqueID)
    {
        GameObject card = CreateACardAtPosition(cardAsset, slots.Children[0].transform.localPosition);

        foreach (Transform transform in card.GetComponentsInChildren<Transform>())
            transform.tag = owner.ToString() + "Card";
        AddCard(card);

        WhereIsCard whereIsCard = card.GetComponent<WhereIsCard>();

        IDHolder id = card.AddComponent<IDHolder>();
        id.UniqueID = UniqueID;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(card.transform.DOLocalMove(slots.Children[0].transform.localPosition, 0));
        if (TakeCardsOpenly)
            sequence.Insert(0f, card.transform.DORotate(Vector3.zero, 0));
        
        sequence.OnComplete(() => ChangeLastCardStatusToInHand(card, whereIsCard));
    }

    void ChangeLastCardStatusToInHand(GameObject card, WhereIsCard whereIsCard)
    {
        if (owner == AreaPosition.Bottom)
            whereIsCard.VisualState = VisualStates.BottomPlayerHand;
        else
            whereIsCard.VisualState = VisualStates.TopPlayerHand;

        whereIsCard.SetHandSortingOrder();
        Command.CommandExecutionComplete();
    }

    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
