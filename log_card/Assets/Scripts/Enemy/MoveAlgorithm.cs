using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveAlgorithm : MonoBehaviour
{
    private Tilemap tilemap;
    bool[,] arr = new bool[1000,1000];
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameManager.instance.tilemap;
        tilemap.GetComponent<TilemapCollider2D>().usedByComposite = false;
        for(int i = 0; i <= 50; i++)
        {
            for(int j = 0; j <= 50; j++)
            {

                if (RayCastTile(i, j))
                {
                    arr[i,j] = true;
                }
            }
        }
        tilemap.GetComponent<TilemapCollider2D>().usedByComposite = true;
        List<Vector2>  r = FindRoot(new Vector2(5,10),new Vector2(20,20));
        //Debug.Log(r.Count);
        for(int i = 0; i < r.Count; i++)
        {
            //Debug.Log(i + ":" + r[i]);
            GameManager.instance.makePing(r[i]);
        }
    }
    public List<Vector2> FindRoot(Vector2 startPos, Vector2 endPos)
    {
        List<Vector2> root = new List<Vector2>();
        Vector2[] dxy = new Vector2[4];
        dxy[0] = new Vector2(0, 1);
        dxy[1] = new Vector2(0, -1);
        dxy[2] = new Vector2(1, 0);
        dxy[3] = new Vector2(-1, 0);
        int[,] check = new int[1000, 1000];
        Vector2[,] from = new Vector2[1000, 1000];
        check[(int)startPos.x, (int)startPos.y] = 1;
        Queue<Vector2> que = new Queue<Vector2>();
        que.Enqueue(startPos);

        while (que.Count > 0)
        {
            Vector2 data = que.Dequeue();
            for(int i = 0; i < 4; i++)
            {
                int random1 = Random.Range(0, 4);
                int random2 = Random.Range(0, 4);

                Vector2 t = dxy[random1];
                dxy[random1] = dxy[random2];
                dxy[random2] = t;
            }
            for (int i = 0; i < 4; i++)
            {
                Vector2 newData = data + dxy[i];
                if(newData.x>=0&&newData.y>=0&&newData.x<=100&&newData.y<=100
                    &&!arr[(int)newData.x, (int)newData.y]&&check[(int)newData.x, (int)newData.y]==0){
                    que.Enqueue(newData);
                    check[(int)newData.x, (int)newData.y] = check[(int)data.x, (int)data.y] + 1;
                    from[(int)newData.x, (int)newData.y] = data;
                }
                if (newData == endPos)
                {
                    Vector2 ansData = endPos;
                    while (ansData != startPos)
                    {
                        root.Add(ansData);
                        ansData = from[(int)ansData.x, (int)ansData.y];
                    }
                    root.Reverse();
                    return root;
                }
            }
        }


        return root;
    }
    bool RayCastTile(int x,int y)
    {
        
        RaycastHit2D[] hit;
        Vector2 vector2 = new Vector2((float)x, (float)y);
        Ray2D ray = new Ray2D(vector2,transform.forward);
        hit = Physics2D.RaycastAll(vector2, transform.forward);
        Debug.DrawRay(vector2, transform.forward * 10, Color.red, 0.3f);
        Debug.Log(hit.Length);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i])
            {
                if (hit[i].transform.GetComponent<TilemapCollider2D>() != null)
                {
                    //Debug.Log(x + "," + y + "," + hit.point);
                    GameManager.instance.makePing(hit[i].point);
                    return true;
                }
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
