using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    List<GameObject> shopCards;
    // Start is called before the first frame update
    void Start()
    {
        shopCards = new List<GameObject>();
        shopCards.Add(GameManager.instance.DropCard(new Vector2(15, 15),true));
        shopCards.Add(GameManager.instance.DropCard(new Vector2(20, 15), true));
        shopCards.Add(GameManager.instance.DropCard(new Vector2(25, 15), true));
    }
    public void DeleteAll()
    {
        for(int i = 0; i < shopCards.Count; i++)
        {
            Destroy(shopCards[i]);
        }
    }
}
