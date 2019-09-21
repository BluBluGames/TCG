using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds list of cards in deck.
/// </summary>
/// <remarks>
/// On awake it shuffles cards in the deck
/// </remarks>
public class DeckController : MonoBehaviour
{
    ///<value>Holds all cards in deck as List</value>
    public List<CardAsset> cards = new List<CardAsset>();

    void Awake()
    {
        ShuffleCards(cards);
    }

    /// <summary>
    /// Shuffles cards in deck
    /// </summary>
    /// <param name="cardsInDeck"></param>
    private void ShuffleCards(List<CardAsset> cardsInDeck)
    {
        System.Random rngGG = new System.Random();

        int n = cardsInDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rngGG.Next(n + 1);
                CardAsset value = cardsInDeck[k];
                cardsInDeck[k] = cardsInDeck[n];
                cardsInDeck[n] = value;
        }
    }
}
