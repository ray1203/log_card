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
    public Card()
    {
        cost = 0;
        name = "";
        txt = "";
    }
    public Card(int cost,string name, string txt, emptyFunc func)
    {
        this.cost = cost;
        this.name = name;
        this.txt = txt;
        this.func = func;
    }
}
