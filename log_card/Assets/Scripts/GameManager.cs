using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerCtrl player;
    public Canvas canvas;
    public Grid grid;
    public Tilemap tilemap;
    public GameObject ping;
    public MoveAlgorithm moveAlgorithm;
    public MapCtrl mapCtrl;
    public CardData cardData;
    public Deck deck;
    public List<Sprite> cardSprites;
    public MyCard myCard;
    public GameObject crosshair;
    public GameObject explode;
    public GameObject fireball;
    public GameObject iceShield;
    public GameObject bullet;
    public GameObject droppedCard;
    public GameObject deckUI;
    public float shuffleTimer;
    public List<GameObject> droppedCards;
    // Start is called before the first frame update
    void Awake()
    {
        shuffleTimer = 0;
        instance = this;
        moveAlgorithm = GetComponent<MoveAlgorithm>();
        mapCtrl = GetComponent<MapCtrl>();
        cardData = new CardData();
        cardData.DataSet();
        deck = new Deck();
        droppedCards = new List<GameObject>();
    }
    private void Start()
    {
        for (int i = 0; i < cardData.cards.Count/2; i++)
            deck.AddCard(cardData.cards[i]);
    }
    private void Update()
    {
        if (deck.shuffleCool)
        {
            shuffleTimer += Time.deltaTime;
            deck.image.fillAmount = 1-shuffleTimer / (float)deck.shuffleTime;
            if (shuffleTimer >= deck.shuffleTime)
            {
                shuffleTimer = 0;
                deck.shuffleCool = false;
            }
        }
    }
    public void MakePing(Vector2 p,GameObject gameObject,float time=1f)
    {
        GameObject newPing = Instantiate(gameObject);
        newPing.transform.position = p;
        StartCoroutine(Timer(time, newPing));
    }
    IEnumerator Timer(float time,GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    public bool CheckWall(Vector2 pos1,Vector2 pos2)
    {
        Vector2 heading = pos2 - pos1;
        float dist = Vector2.Distance(pos1, pos2);
        Vector2 direction = heading / dist;
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos1, direction, dist);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer==7) { return true;}
        }
        return false;
    }
    public GameObject DropCard(Vector2 pos,bool shopCard = false)
    {
        GameObject newObject = Instantiate(droppedCard);
        newObject.transform.SetParent(canvas.transform);
        newObject.GetComponent<DroppedCard>().RandomSetting();
        newObject.GetComponent<DroppedCard>().pos = pos;
        newObject.GetComponent<DroppedCard>().shopCard = shopCard;
        droppedCards.Add(newObject);

        return newObject;
    }
    public void EraseDroppedCards()
    {
        Debug.Log("droppedCards: " + droppedCards.Count);
        for(int i = 0; i < droppedCards.Count; i++)
        {
            Destroy(droppedCards[i]);
        }
    }
}
