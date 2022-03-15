using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardData
{
    public List<Card> cards;
    public void DataSet()
    {
        cards = new List<Card>();
        cards.Add(new Card(2, "회복", "체력을 10 회복시킵니다", delegate 
        { 
            GameManager.instance.player.Heal(10); 
        }, GameManager.instance.cardSprites[0]));

        cards.Add(new Card(6, "대폭발", "폭발합니다.", delegate
        {
            GameObject newObject = GameObject.Instantiate(GameManager.instance.explode);
            newObject.GetComponentInChildren<Explode>().damage = 50;
            newObject.GetComponentInChildren<Explode>().SetScale(5);
            newObject.transform.position = GameManager.instance.player.transform.position;
        }, GameManager.instance.cardSprites[1]));

        cards.Add(new Card(5, "화염구", "화염구를 던저 터뜨립니다.", delegate
        {
            GameObject newObject = GameObject.Instantiate(GameManager.instance.fireball);
            newObject.transform.position = GameManager.instance.player.transform.position;
            newObject.GetComponent<FireBall>().damage = 30;
            newObject.GetComponent<FireBall>().pos = GameManager.instance.crosshair.transform.position;
        }, GameManager.instance.cardSprites[2], true));

        cards.Add(new Card(3, "경량화", "더 빨라집니다.", delegate
        { 
            BuffManager.instance.addBuff(BuffStat.speed, 0.5f, 20); 
        }, GameManager.instance.cardSprites[3]));

        cards.Add(new Card(3, "강철 신체", "느려지지만 피해를 덜 받습니다.", delegate
        {
            BuffManager.instance.addBuff(BuffStat.speed, -0.3f, 20);
            BuffManager.instance.addBuff(BuffStat.def, 0.5f, 20);
        }, GameManager.instance.cardSprites[4]));

        cards.Add(new Card(3, "얼음 방패", "잠시 무적이 됩니다.", delegate
           {
               BuffManager.instance.addBuff(BuffStat.stop, 1f, 1);
               BuffManager.instance.addBuff(BuffStat.absolDef, 10000000, 1);
               GameManager.instance.makePing(GameManager.instance.player.transform.position, GameManager.instance.iceShield) ;
           }, GameManager.instance.cardSprites[5]));
    }

}
