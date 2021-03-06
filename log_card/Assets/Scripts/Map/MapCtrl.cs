using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public enum RoomType {battle,shop,heal,boss,victory};
    public int mapCount;
    public List<GameObject> battleMaps;
    public GameObject healMap;
    public GameObject shopMap;
    public GameObject bossMap;
    private GameObject currentMap=null;
    private GameObject gate1, gate2;
    private GameObject icon1, icon2;
    private GameObject enemy;
    private GameObject block;
    public Sprite shopIcon, healIcon,bossIcon;
    private bool flag=true;//true: ?? ???? ?????? 
    void Start()
    {
        CreateMap(0);
        mapCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!flag)
            EnemyCheck();
    }
    public void CreateMap(RoomType type)//0: battle 1: shop 2: heal 3:boss
    {
        mapCount++;
        GameManager.instance.EraseDroppedCards();
        switch (type)
        {
            case RoomType.battle:
                int rand = Random.Range(0, battleMaps.Count);
                if (currentMap != null) Destroy(currentMap);
                currentMap = Instantiate(battleMaps[rand]);
                
                break;
            case RoomType.heal:
                if (currentMap != null) Destroy(currentMap);
                currentMap = Instantiate(healMap);
                break;
            case RoomType.shop:
                if (currentMap != null) Destroy(currentMap);
                currentMap = Instantiate(shopMap);
                break;
            case RoomType.boss:
                if (currentMap != null) Destroy(currentMap);
                currentMap = Instantiate(bossMap);
                break;

            default:break;
        }
        GameManager.instance.grid = currentMap.transform.Find("Grid").GetComponent<Grid>();
        GameManager.instance.tilemap = GameManager.instance.grid.transform.Find("wall").GetComponent<Tilemap>();
        gate1 = GameManager.instance.grid.transform.Find("gate1").gameObject;
        gate2 = GameManager.instance.grid.transform.Find("gate2").gameObject;
        gate1.SetActive(false);
        gate2.SetActive(false);
        icon1 = GameManager.instance.grid.transform.Find("icon1").gameObject;
        icon2 = GameManager.instance.grid.transform.Find("icon2").gameObject;
        enemy = currentMap.transform.Find("Enemy").gameObject;
        block = GameManager.instance.grid.transform.Find("block").gameObject;
        GameManager.instance.player.transform.position = new Vector2(21, 9);
        StartCoroutine("ActivateSetMap");
        flag = false;
    }
    IEnumerator ActivateSetMap()
    {

        yield return new WaitForSeconds(0.05f);
        GameManager.instance.moveAlgorithm.SetMap();

    }
    public void EnemyCheck()
    {
        if (enemy.transform.childCount ==0)
        {
            Destroy(block);
            gate1.GetComponent<TilemapCollider2D>().isTrigger = true;
            gate2.GetComponent<TilemapCollider2D>().isTrigger = true;
            int rand = Random.Range(1,11);
            if (mapCount == 10) rand = -1;
            if (mapCount == 11) rand = -2;
            if (rand == 1)
            {
                icon1.GetComponent<SpriteRenderer>().sprite = shopIcon;
                gate1.GetComponent<Gate>().roomType = RoomType.shop;
            }else if(rand == 2)
            {
                icon1.GetComponent<SpriteRenderer>().sprite = healIcon;
                gate1.GetComponent<Gate>().roomType = RoomType.heal;
            }
            else if (rand == -1)
            {
                icon1.GetComponent<SpriteRenderer>().sprite = bossIcon;
                gate1.GetComponent<Gate>().roomType = RoomType.boss;
            }
            else if (rand == -2)
            {
                icon1.GetComponent<SpriteRenderer>().sprite = null;
                gate1.GetComponent<Gate>().roomType = RoomType.victory;
            }
            else
            {
                icon1.GetComponent<SpriteRenderer>().sprite = null;
                gate1.GetComponent<Gate>().roomType = RoomType.battle;
            }
            rand = Random.Range(1, 11);
            if (mapCount == 10) rand = -1;
            if (mapCount == 11) rand = -2;
            if (rand == 1)
            {
                icon2.GetComponent<SpriteRenderer>().sprite = shopIcon;
                gate2.GetComponent<Gate>().roomType = RoomType.shop;
            }
            else if (rand == 2)
            {
                icon2.GetComponent<SpriteRenderer>().sprite = healIcon;
                gate2.GetComponent<Gate>().roomType = RoomType.heal;
            }else if (rand == -1)
            {
                icon2.GetComponent<SpriteRenderer>().sprite = bossIcon;
                gate2.GetComponent<Gate>().roomType = RoomType.boss;
            }
            else if (rand == -2)
            {
                icon2.GetComponent<SpriteRenderer>().sprite = null;
                gate2.GetComponent<Gate>().roomType = RoomType.victory;
            }
            else
            {
                icon2.GetComponent<SpriteRenderer>().sprite = null;
                gate2.GetComponent<Gate>().roomType = RoomType.battle;
            }

            flag = true;
            gate1.SetActive(true);
            gate2.SetActive(true);
        }
    }
}
