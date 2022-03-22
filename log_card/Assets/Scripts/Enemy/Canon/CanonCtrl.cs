using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonCtrl : MonoBehaviour
{
    private int maxHp = 10;
    private int damage = 10;
    private float bulletSpeed = 5f;
    private float attackDist = 10f;
    private float attackCool = 3f;
    private float attackTimer = 3f;

    private void Start()
    {
        
    }
    private void Update()
    {
        attackTimer += Time.deltaTime;
        if(!GameManager.instance.CheckWall(this.transform.position, GameManager.instance.player.transform.position))
        {
            Debug.Log("a");
            if (attackTimer >= attackCool && Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= attackDist)
            {
                Debug.Log("B");
                attackTimer = 0f;
                GameObject newObject = Instantiate(GameManager.instance.bullet);
                
                newObject.GetComponent<Bullet>().damage = damage;
                newObject.GetComponent<Bullet>().speed = bulletSpeed;
                Vector2 pos1 = this.transform.position;
                newObject.transform.position = pos1;
                Vector2 pos2 = GameManager.instance.player.transform.position;
                Vector2 pos3 = pos2 - pos1;
                newObject.GetComponent<Bullet>().targetPos = pos1+pos3*1000f;
            }
        }
    }
}
