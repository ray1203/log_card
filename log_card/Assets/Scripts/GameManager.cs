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
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        moveAlgorithm = GetComponent<MoveAlgorithm>();
        mapCtrl = GetComponent<MapCtrl>();
        cardData = new CardData();
        cardData.DataSet();
        deck = new Deck();
    }
    private void Start()
    {
        for (int i = 0; i < cardData.cards.Count; i++)
            deck.AddCard(cardData.cards[i]);
    }
    private void Update()
    {
    }
    public void makePing(Vector2 p,GameObject gameObject,float time=1f)
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
    public bool checkWall(Vector2 pos1,Vector2 pos2)
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
}
