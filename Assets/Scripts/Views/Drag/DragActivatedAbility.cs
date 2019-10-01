using UnityEngine;
using System.Collections;

public class DragActivatedAbility : DraggingActions {

    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;
    private WhereIsCard whereIsThisCard;
    private Transform triangle;
    private SpriteRenderer triangleSR;
    private GameObject Target;
    private CardOnBoardView cardView;

    // Core functionality
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.sortingLayerName = "AboveEverything";
        triangle = transform.Find("Triangle");
        triangleSR = triangle.GetComponent<SpriteRenderer>();

        cardView = GetComponentInParent<CardOnBoardView>();
        whereIsThisCard = GetComponentInParent<WhereIsCard>();
    }

    public override bool CanDrag
    {
        get
        {
            return base.CanDrag && cardView.IsAbleToUseAbility;
        }
    }

    public override void OnStartDrag()
    {
        whereIsThisCard.VisualState = VisualStates.Dragging;
        spriteRenderer.enabled = false;
        lineRenderer.enabled = true;
    }

    public override void OnDraggingInUpdate()
    {
        RenderTargetingArrow();
    }

    protected override int DragSuccessful()
    {
        return 1;
    }

    public override void OnEndDrag()
    {
        GetRaycastTarget();

        bool targetValid = false;

        UseAbilityOnSelectedTarget();
        HandleInvalidTarget(targetValid);

        RemoveTargetLine();
    }

    //Helpers

    private void RemoveTargetLine()
    {
        transform.localPosition = Vector3.zero;
        spriteRenderer.enabled = false;
        lineRenderer.enabled = false;
        triangleSR.enabled = false;
    }

    private void HandleInvalidTarget(bool targetValid)
    {
        if (!targetValid)
        {
            if (tag.Contains("LowBoardFront"))
                whereIsThisCard.VisualState = VisualStates.LowBoardFrontRow;
            else if (tag.Contains("LowBoardBack"))
                whereIsThisCard.VisualState = VisualStates.LowBoardBackRow;
            else if (tag.Contains("TopBoardFront"))
                whereIsThisCard.VisualState = VisualStates.TopBoardFrontRow;
            else if (tag.Contains("TopBoardBack"))
                whereIsThisCard.VisualState = VisualStates.TopBoardBackRow;

            whereIsThisCard.SetTableSortingOrder();
        }
    }

    private void UseAbilityOnSelectedTarget()
    {
        if (Target != null)
        {
            int targetID = Target.GetComponent<IDHolder>().UniqueID;
            if (CardOnBoardController.CardsPlayedThisGame[targetID] != null)
            {
                CardOnBoardController.CardsPlayedThisGame[GetComponentInParent<IDHolder>().UniqueID].UseAbilityOnTargetID(targetID);
            }
        }
    }

    private void GetRaycastTarget()
    {
        Target = null;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(origin: Camera.main.transform.position,
            direction: (-Camera.main.transform.position + this.transform.position).normalized,
            maxDistance: 30f);

        foreach (RaycastHit hit in hits)
        {
            if (((hit.transform.tag == "BottomCardOnFrontRow" || hit.transform.tag == "BottomCardOnBackRow") && (tag == "TopCardOnFrontRow" || tag == "TopCardOnBackRow")) ||
                ((hit.transform.tag == "TopCardOnFrontRow" || hit.transform.tag == "TopCardOnBackRow") && (tag == "BottomCardOnFrontRow" || tag == "BottomCardOnBackRow")))
            {
                Target = hit.transform.parent.gameObject;
            }
        }
    }

    private void RenderTargetingArrow()
    {
        Vector3 notNormalized = transform.position - transform.parent.position;
        Vector3 direction = notNormalized.normalized;
        float distanceToTarget = (direction * 2.3f).magnitude;

        lineRenderer.SetPositions(new Vector3[] { transform.parent.position, transform.position });
        lineRenderer.enabled = true;

        triangleSR.enabled = true;
        triangleSR.transform.position = transform.position;

        float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
        triangleSR.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
