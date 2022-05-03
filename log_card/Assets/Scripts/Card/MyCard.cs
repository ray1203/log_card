using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MyCard : MonoBehaviour
{
    public List<CardCtrl> cardObjects;
    public GameObject emptyCard;
    public GameObject myCard;
    private float scaleSet = 1f;
    private void Start()
    {
        cardObjects = new List<CardCtrl>();
        scaleSet = Screen.width / 2560.0f;
    }
    private void Update()
    {
        CardCtrl selectedCard = RaycastCard();
        if (selectedCard != null)
        {
            Time.timeScale = 0.00f;
            selectedCard.control = true;


        }
    }
    public void AddCard(Card newCard)
    {
        if (newCard == null) return;
        GameObject newCardObj = Instantiate(emptyCard);
        newCardObj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = newCard.name;
        newCardObj.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = newCard.cost.ToString();
        newCardObj.transform.Find("Desc").GetComponent<TextMeshProUGUI>().text = newCard.txt;
        newCardObj.transform.Find("Image").GetComponent<Image>().sprite = newCard.sprite;
        newCardObj.transform.SetParent(myCard.transform);
        newCardObj.transform.localScale = new Vector2(scaleSet, scaleSet);
        newCardObj.transform.localPosition = new Vector3(0,0,-1);
        newCardObj.GetComponent<CardCtrl>().card = newCard;
        cardObjects.Add(newCardObj.GetComponent<CardCtrl>());
        SetPos();

    }
    public void SetPos()
    {
        int num = cardObjects.Count-1;
        List<Vector2> pos = new List<Vector2>();
        int startX = -(int)(250.0f*scaleSet) * num / 2;
        int endX = (int)(250.0f * scaleSet) * num / 2+1;
        for(int i = startX; i <= endX; i += (int)(250.0f * scaleSet))
        {
            pos.Add(new Vector2(i, -300));
        }
        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardObjects[i].pos = pos[i];
        }

    }
    private CardCtrl RaycastCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(pos, Vector2.zero);
            for(int i = 0; i < raycastHit2D.Length; i++)
            {
                if (raycastHit2D[i].transform.GetComponent<CardCtrl>()!=null)
                {
                    return raycastHit2D[i].transform.GetComponent<CardCtrl>();
                }
            }
        }
        return null;
    }
    
}
