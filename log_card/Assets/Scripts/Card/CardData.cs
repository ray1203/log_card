using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardData
{
    public List<Card> cards;
    public void DataSet()
    {
        cards = new List<Card>();
        cards.Add(new Card(2,"ȸ��","ü���� 10 ȸ����ŵ�ϴ�",delegate { GameManager.instance.player.heal(10); }));
    }
}
