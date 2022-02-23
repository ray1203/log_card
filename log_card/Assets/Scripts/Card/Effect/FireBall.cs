using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector2 pos;
    float speed = 10f;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, speed*Time.deltaTime);
        if (Vector2.Distance(transform.position,pos)<=0.1f)
        {
            GameObject newObject = GameObject.Instantiate(GameManager.instance.explode);
            newObject.GetComponentInChildren<Explode>().damage = damage; 
            newObject.GetComponentInChildren<Explode>().SetScale(3);
            newObject.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
