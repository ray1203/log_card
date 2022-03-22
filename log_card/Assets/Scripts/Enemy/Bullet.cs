using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Vector2 targetPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCtrl>().Damaged(damage);
            Destroy(this.gameObject);
        }
        if (collision.transform.gameObject.layer == 7)
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,targetPos)<=0.1f) Destroy(this.gameObject);
    }
}
