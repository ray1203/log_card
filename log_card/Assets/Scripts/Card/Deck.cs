using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Deck
{
    public List<Card> deck;//����
    public List<Card> curDeck;//ī�� �Ҹ�� ��
    public bool shuffleCool = false;
    public int shuffleTime = 10;
    private Text text;//���� �� �ż� ǥ�� �ؽ�Ʈ
    public Image image;//�� ������ �̹���
    public Deck()
    {
        shuffleCool = false;
        deck = new List<Card>();
        curDeck = new List<Card>();
        text = GameManager.instance.deckUI.transform.Find("Text").GetComponent<Text>();
        image = GameManager.instance.deckUI.transform.Find("Image").GetComponent<Image>();
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
        shuffleCool = true;
    }
    public Card Draw()
    {
        if (shuffleCool||GameManager.instance.myCard.cardObjects.Count >= 6) return null;
        Card card = curDeck[0];
        curDeck.RemoveAt(0);
        if (curDeck.Count == 0)
        {
            curDeck = new List<Card>(deck.ToArray());
            Shuffle();
        }
        text.text = curDeck.Count + "/" + deck.Count;
        return card;
    }
    public void AddCard(Card card)
    {
        deck.Add(card);
        curDeck.Add(card);
        text.text = curDeck.Count + "/" + deck.Count;
    }
}
