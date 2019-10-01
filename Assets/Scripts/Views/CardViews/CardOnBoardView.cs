using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOnBoardView : MonoBehaviour
{
    public CardAsset cardAsset;

    [Header("Card Body")]
    public Text NameText;
    public Text healthText;
    public Image cardArtImage;
    public Image cardFrameImage;
    public Image cardIconImage;
    public Image cardGlowImage;

    [Header("CardDescription")]
    public Text cardAbilities;

    void Awake()
    {
        if (cardAsset != null)
            displayCardOnBoardVisual();
    }

    private bool isAbleToUseAbility = true;
    public bool IsAbleToUseAbility { get => isAbleToUseAbility; set => isAbleToUseAbility = value; }

    public void displayCardOnBoardVisual()
    {
        NameText.text = cardAsset.name;
        cardArtImage.sprite = cardAsset.cardArt;

        if (cardAsset.cardHealth != 0)
        {
            healthText.text = cardAsset.cardHealth.ToString();
        }

        if (cardAsset.cardAbility != null)
        {
            cardAbilities.text = cardAsset.cardAbility.ToString();
        }
    }
}
