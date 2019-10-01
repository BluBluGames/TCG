using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoView : MonoBehaviour
{
    public Faction faction;
    [Header("Text Component References")]
    public Text NameText;

    [Header("Image References")]
    public Image portraitImage;

    void Awake()
    {
        if (faction != null)
            ApplyLookFromAsset();
    }

    public void ApplyLookFromAsset()
    {
        portraitImage.sprite = faction.icon;
    }
}
