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
    public Card()
    {
        cost = 0;
        name = "";
        txt = "";
        sprite = null;
    }
    public Card(int cost,string name, string txt, emptyFunc func,Sprite sprite)
    {
        this.cost = cost;
        this.name = name;
        this.txt = txt;
        this.func = func;
        this.sprite = sprite;
        printData();
    }
    public void printData()
    {
        Debug.Log(cost + " " + name + " " + txt + " " + sprite.name);
    }
}
