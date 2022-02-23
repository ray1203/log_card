using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardData
{
    public List<Card> cards;
    public void DataSet()
    {
        cards = new List<Card>();
        cards.Add(new Card(2,"ȸ��","ü���� 10 ȸ����ŵ�ϴ�",delegate { GameManager.instance.player.heal(10);},GameManager.instance.cardSprites[0]));
        cards.Add(new Card(6, "������", "�����մϴ�.", delegate { GameObject newObject = GameObject.Instantiate(GameManager.instance.explode);newObject.GetComponentInChildren<Explode>().damage = 50; newObject.GetComponentInChildren<Explode>().SetScale(5);newObject.transform.position = GameManager.instance.player.transform.position; }, GameManager.instance.cardSprites[1]));
        cards.Add(new Card(5, "ȭ����", "ȭ������ ���� �Ͷ߸��ϴ�.", delegate { GameObject newObject = GameObject.Instantiate(GameManager.instance.fireball);newObject.transform.position = GameManager.instance.player.transform.position; newObject.GetComponent<FireBall>().damage = 30;newObject.GetComponent<FireBall>().pos = GameManager.instance.crosshair.transform.position; }, GameManager.instance.cardSprites[2],true));

    }

}
