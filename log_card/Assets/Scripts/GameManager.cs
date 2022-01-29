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
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        tilemap = grid.transform.Find("wall").GetComponent<Tilemap>();
    }
    private void Update()
    {
    }
    public void makePing(Vector2 p)
    {
        GameObject newPing = Instantiate(ping);
        ping.transform.position = p;
    }
}
