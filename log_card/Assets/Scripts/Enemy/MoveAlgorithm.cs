using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveAlgorithm : MonoBehaviour
{
    private Tilemap tilemap;
    bool[,] arr = new bool[1000,1000];//true: Àå¾Ö¹°
    // Start is called before the first frame update
    void Start()
    {
        //List<Vector2>  r = FindRoot(new Vector2(5,10),new Vector2(20,20));
        //Debug.Log(r.Count);
        //for(int i = 0; i < r.Count; i++)
        //{
            //Debug.Log(i + ":" + r[i]);
         //   GameManager.instance.makePing(r[i]);
        //}
    }
    public void SetMap()
    {
        tilemap = GameManager.instance.tilemap;
        tilemap.GetComponent<TilemapCollider2D>().usedByComposite = false;
        for (int i = 0; i <= 50; i++)
        {
            for (int j = 0; j <= 50; j++)
            {

                if (RayCastTile(i, j))
                {
                    arr[i, j] = true;
                }
            }
        }
        tilemap.GetComponent<TilemapCollider2D>().usedByComposite = true;
    }
    public Vector2[] FindRoot(Vector2 startPos, Vector2 endPos)
    {
        startPos = new Vector2((int)startPos.x, (int)startPos.y);
        endPos = new Vector2((int)endPos.x, (int)endPos.y);
        Vector2[] dxy = new Vector2[4];
        dxy[0] = new Vector2(0, 1);
        dxy[1] = new Vector2(0, -1);
        dxy[2] = new Vector2(1, 0);
        dxy[3] = new Vector2(-1, 0);
        if(arr[(int)endPos.x, (int)endPos.y])
        {
            for(int i = 0; i < 4; i++)
            {
                Vector2 newData = endPos + dxy[i];
                if (!arr[(int)newData.x, (int)newData.y])
                {
                    endPos = newData;
                    break;
                }
            }
        }
        int[,] check = new int[1000, 1000];
        Vector2[,] from = new Vector2[1000, 1000];
        check[(int)startPos.x, (int)startPos.y] = 1;
        Queue<Vector2> que = new Queue<Vector2>();
        que.Enqueue(startPos);

        while (que.Count > 0)
        {
            //Debug.Log(que.Count);
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
                if (CompareIntVector(newData,endPos))
                {
                    int cap = check[(int)newData.x, (int)newData.y];
                    Vector2[] root = new Vector2[cap+1];
                    Vector2 ansData = endPos;
                    int idx = cap-1;
                    while (ansData != startPos)
                    {
                        root[--idx] = new Vector2((int)ansData.x,(int)ansData.y);
                        ansData = from[(int)ansData.x, (int)ansData.y];
                        if (idx == 0) break;
                    }
                    return root;
                }
            }
        }


        return null;
    }
    public Vector2[] FindRandomRoot(Vector2 startPos)
    {
        startPos = new Vector2((int)startPos.x, (int)startPos.y);
        Vector2[] dxy = new Vector2[4];
        dxy[0] = new Vector2(0, 1);
        dxy[1] = new Vector2(0, -1);
        dxy[2] = new Vector2(1, 0);
        dxy[3] = new Vector2(-1, 0);

        Vector2[] root = new Vector2[10];
        root[0] = startPos;
        for(int i = 1; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int random1 = Random.Range(0, 4);
                int random2 = Random.Range(0, 4);

                Vector2 t = dxy[random1];
                dxy[random1] = dxy[random2];
                dxy[random2] = t;
            }
            for (int j=0;j<4;j++)
            {
                Vector2 newRoot = root[i - 1] + dxy[j];
                if (arr[(int)newRoot.x, (int)newRoot.y]||GameManager.instance.CheckWall(newRoot, startPos)){
                    continue;
                }
                else
                {
                    root[i] = newRoot;
                    break;
                }
            }
        }
        return root;
    }
    bool CompareIntVector(Vector2 vec1,Vector2 vec2)
    {
        if ((int)vec1.x == (int)vec2.x && (int)vec1.y == (int)vec2.y) return true;
        return false;
    }
    bool RayCastTile(int x,int y)
    {
        
        RaycastHit2D[] hit;
        Vector2 vector2 = new Vector2((float)x, (float)y);
        Ray2D ray = new Ray2D(vector2,transform.forward);
        hit = Physics2D.RaycastAll(vector2, transform.forward);
        Debug.DrawRay(vector2, transform.forward * 10, Color.red, 0.3f);
        
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i])
            {
                if (hit[i].transform.GetComponent<TilemapCollider2D>() != null && !hit[i].transform.GetComponent<TilemapCollider2D>().isTrigger)
                {
                    //Debug.Log(x + "," + y + "," + hit.point);
                    //GameManager.instance.makePing(hit[i].point);
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
