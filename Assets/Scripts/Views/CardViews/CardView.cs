using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
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
            displayCardView();
    }

    private bool isAllowedToBePlayed = true;

    public bool IsAllowedToBePlayed { get => isAllowedToBePlayed; set => isAllowedToBePlayed = value; }

    public void displayCardView()
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
