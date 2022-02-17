using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MyCard : MonoBehaviour
{
    public List<Card> cards;
    public List<GameObject> cardObjects;
    public GameObject emptyCard;
    public GameObject myCard;
    List<Vector2> cardPos;
    private void Start()
    {
        cards = new List<Card>();
        cardObjects = new List<GameObject>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) AddCard(GameManager.instance.cardData.cards[0]);
        for(int i = 0; i < cardObjects.Count; i++)
        {
            cardObjects[i].transform.localPosition = cardPos[i];
        }
    }
    public void AddCard(Card newCard)
    {
        cards.Add(newCard);
        GameObject newCardObj = Instantiate(emptyCard);
        newCardObj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = newCard.name;
        newCardObj.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = newCard.cost.ToString();
        newCardObj.transform.Find("Desc").GetComponent<TextMeshProUGUI>().text = newCard.txt;
        newCardObj.transform.Find("Image").GetComponent<Image>().sprite = newCard.sprite;
        newCardObj.transform.parent = myCard.transform;
        newCardObj.transform.localScale = new Vector2(1, 1);
        newCardObj.transform.localPosition = new Vector3(0,0,-1);
        cardObjects.Add(newCardObj);
        cardPos = SetPos(cards.Count);
    }
    public List<Vector2> SetPos(int num)
    {
        List<Vector2> pos = new List<Vector2>();
        int startX = -250 * num / 2;
        int endX = 250 * num / 2;
        for(int i = startX; i <= endX; i += 250)
        {
            pos.Add(new Vector2(i+125, -300));
        }
        return pos;
    }
    
}
