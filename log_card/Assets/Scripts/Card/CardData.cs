using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardData
{
    public List<Card> cards;
    public void DataSet()
    {
        cards = new List<Card>();
        cards.Add(new Card(2, "ȸ��", "ü���� 10 ȸ����ŵ�ϴ�", delegate 
        { 
            GameManager.instance.player.Heal(10); 
        }, GameManager.instance.cardSprites[0]));

        cards.Add(new Card(6, "������", "�����մϴ�.", delegate
        {
            GameObject newObject = GameObject.Instantiate(GameManager.instance.explode);
            newObject.GetComponentInChildren<Explode>().damage = 50;
            newObject.GetComponentInChildren<Explode>().SetScale(5);
            newObject.transform.position = GameManager.instance.player.transform.position;
        }, GameManager.instance.cardSprites[1]));

        cards.Add(new Card(5, "ȭ����", "ȭ������ ���� �Ͷ߸��ϴ�.", delegate
        {
            GameObject newObject = GameObject.Instantiate(GameManager.instance.fireball);
            newObject.transform.position = GameManager.instance.player.transform.position;
            newObject.GetComponent<FireBall>().damage = 30;
            newObject.GetComponent<FireBall>().pos = GameManager.instance.crosshair.transform.position;
        }, GameManager.instance.cardSprites[2], true));

        cards.Add(new Card(3, "�淮ȭ", "�� �������ϴ�.", delegate
        { 
            BuffManager.instance.AddBuff(BuffStat.speed, 1.5f, 20); 
        }, GameManager.instance.cardSprites[3]));

        cards.Add(new Card(3, "��ö ��ü", "���������� ���ظ� �� �޽��ϴ�.", delegate
        {
            BuffManager.instance.AddBuff(BuffStat.speed, 0.7f, 20);
            BuffManager.instance.AddBuff(BuffStat.def, 1.5f, 20);
        }, GameManager.instance.cardSprites[4]));

        cards.Add(new Card(3, "���� ����", "��� ������ �˴ϴ�.", delegate
           {
               BuffManager.instance.AddBuff(BuffStat.stop, 1f, 1);
               BuffManager.instance.AddBuff(BuffStat.absolDef, 10000000, 1);
               GameManager.instance.MakePing(GameManager.instance.player.transform.position, GameManager.instance.iceShield) ;
           }, GameManager.instance.cardSprites[5]));

        cards.Add(new Card(6, "����ȭ", "���ݷ°� �̵��ӵ��� �����մϴ�.", delegate
           {
               BuffManager.instance.AddBuff(BuffStat.damage, 1.3f, 10);
               BuffManager.instance.AddBuff(BuffStat.speed, 1.2f, 10);
           }, GameManager.instance.cardSprites[6]
        ));

        cards.Add(new Card(4, "�����̵�", "�ش� ��ġ�� �̵��մϴ�.", delegate
           {
               Vector3 p =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
               GameManager.instance.player.transform.position = new Vector3(p.x, p.y, 0);
           }, GameManager.instance.cardSprites[7], true));

    }

}
