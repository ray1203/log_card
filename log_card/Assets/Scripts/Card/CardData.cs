using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardData
{
    public List<Card> cards;
    public void DataSet()
    {
        cards = new List<Card>();
        cards.Add(new Card(2,"회복","체력을 10 회복시킵니다",delegate { GameManager.instance.player.heal(10);},GameManager.instance.cardSprites[0]));
        cards.Add(new Card(6, "대폭발", "폭발합니다.", delegate { GameObject newObject = GameObject.Instantiate(GameManager.instance.explode);newObject.GetComponentInChildren<Explode>().damage = 50; newObject.GetComponentInChildren<Explode>().SetScale(5);newObject.transform.position = GameManager.instance.player.transform.position; }, GameManager.instance.cardSprites[1]));


    }

}
