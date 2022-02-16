using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardData
{
    public List<Card> cards;
    public void DataSet()
    {
        cards = new List<Card>();
        cards.Add(new Card(2,"회복","체력을 10 회복시킵니다",delegate { GameManager.instance.player.heal(10); }));
    }
}
