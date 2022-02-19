using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class Card
{
    public delegate void emptyFunc();
    public emptyFunc func = delegate { };

    public int cost;
    public string name,txt;
    public Sprite sprite;
    public bool targeting;//위치 지정==true 위치 비지정(힐 등) == false
    public Card()
    {
        cost = 0;
        name = "";
        txt = "";
        sprite = null;
        targeting = false;
    }
    public Card(int cost,string name, string txt, emptyFunc func,Sprite sprite,bool targeting = false)
    {
        this.cost = cost;
        this.name = name;
        this.txt = txt;
        this.func = func;
        this.sprite = sprite;
        this.targeting = targeting;
        printData();
    }
    public void printData()
    {
        Debug.Log(cost + " " + name + " " + txt + " " + sprite.name);
    }
}
