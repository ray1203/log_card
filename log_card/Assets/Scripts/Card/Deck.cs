using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Deck
{
    public List<Card> deck;//µ¦¸®
    public List<Card> curDeck;//Ä«µå ¼Ò¸ðµÈ µ¦
    public Deck()
    {
        deck = new List<Card>();
        curDeck = new List<Card>();
    }
    public void Shuffle()
    {
        int r1, r2;
        Card t;
        for(int i = 0; i < curDeck.Count; i++)
        {
            r1 = Random.Range(0, curDeck.Count);
            r2 = Random.Range(0, curDeck.Count);

            t = curDeck[r1];
            curDeck[r1] = curDeck[r2];
            curDeck[r2] = t;
        }
    }
    public Card Draw()
    {
        
        Card card = curDeck[0];
        curDeck.RemoveAt(0);
        if (curDeck.Count == 0)
        {
            curDeck = new List<Card>(deck.ToArray());
            Shuffle();
        }
        return card;
    }
    public void AddCard(Card card)
    {
        deck.Add(card);
        curDeck.Add(card);
    }
}
