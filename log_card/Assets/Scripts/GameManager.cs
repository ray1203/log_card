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
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        tilemap = grid.transform.Find("wall").GetComponent<Tilemap>();
        moveAlgorithm = gameObject.GetComponent<MoveAlgorithm>();
    }
    private void Start()
    {
        Debug.Log(checkWall(new Vector2(10, 10), new Vector2(10, 20)));
    }
    private void Update()
    {
    }
    public void makePing(Vector2 p)
    {
        GameObject newPing = Instantiate(ping);
        ping.transform.position = p;
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
