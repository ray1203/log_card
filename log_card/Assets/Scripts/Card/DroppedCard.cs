using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DroppedCard : MonoBehaviour
{
    public Card card;
    public Vector2 pos;
    public bool shopCard = false;
    // Start is called before the first frame update
    void Start()
    {
        RandomSetting();

        transform.localScale = new Vector2(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    public void RandomSetting()
    {
        int r = Random.Range(0, GameManager.instance.cardData.cards.Count);
        card = GameManager.instance.cardData.cards[r];
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = card.name;
        transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = card.cost.ToString();
        transform.Find("Desc").GetComponent<TextMeshProUGUI>().text = card.txt;
        transform.Find("Image").GetComponent<Image>().sprite = card.sprite;
        transform.localScale = new Vector2(1, 1);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&Input.GetKey(KeyCode.E))
        {
            GameManager.instance.deck.AddCard(card);
            if (shopCard)
            {
                GameObject.FindObjectOfType<Shop>().DeleteAll();
            }
            else
                Destroy(gameObject);
        }
    }
}
