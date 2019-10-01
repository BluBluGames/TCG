using UnityEngine;
using System.Collections;

public class HoverPreview : MonoBehaviour
{
    public GameObject previewGameObject;
    public GameObject glow;

    private static HoverPreview currentlyViewing = null;

    private static bool _PreviewsAllowed = true;
    public static bool PreviewsAllowed
    {
        get { return _PreviewsAllowed; }

        set
        {
            _PreviewsAllowed = value;
            if (!_PreviewsAllowed)
                StopAllPreviews();
        }
    }

    private bool _thisPreviewEnabled = false;
    public bool ThisPreviewEnabled
    {
        get { return _thisPreviewEnabled; }

        set
        {
            _thisPreviewEnabled = value;
            if (!_thisPreviewEnabled)
                StopThisPreview();
        }
    }

    public bool OverCollider { get; set; }

    void OnMouseEnter()
    {
        OverCollider = true;
        if (PreviewsAllowed && ThisPreviewEnabled)
            PreviewThisObject();
    }

    void OnMouseExit()
    {
        OverCollider = false;
        StopAllPreviews();
    }

    void PreviewThisObject()
    {
        currentlyViewing = this;
        previewGameObject.SetActive(true);
        glow.SetActive(true);
    }

    void StopThisPreview()
    {
        previewGameObject.SetActive(false);
        glow.SetActive(false);
    }

    private static void StopAllPreviews()
    {
        if (currentlyViewing != null)
        {
            currentlyViewing.previewGameObject.SetActive(false);
            currentlyViewing.glow.SetActive(false);
        }
    }
}
