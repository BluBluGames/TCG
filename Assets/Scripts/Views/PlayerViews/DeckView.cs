using UnityEngine;
using System.Collections;

/// <summary>
/// This class number of cards left in deck for visual purposes
/// </summary>
public class DeckView : MonoBehaviour
{
    public AreaPosition owner;

    void Start()
    {
        //CardsInDeck = ConfigManager.Instance.Players[owner].deck.cards.Count;
    }

    private int cardsInDeck = 0;
    public int CardsInDeck
    {
        get { return cardsInDeck; }

        set
        {
            cardsInDeck = value;
        }
    }

}