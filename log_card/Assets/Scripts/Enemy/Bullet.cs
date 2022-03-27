using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Vector2 targetPos;
    private float timer;
    private void Start()
    {
        timer = 10f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCtrl>().Damaged(damage);
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f) Destroy(this.gameObject);
        this.transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,targetPos)<=0.1f) Destroy(this.gameObject);
    }
}
