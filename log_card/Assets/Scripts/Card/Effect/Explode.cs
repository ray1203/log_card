using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField]
    private List<EnemyHp> enemyList;
    [SerializeField]
    private bool playerHit = false;
    public int damage;
    public Vector2 scale = new Vector2(0.5f, 0.5f);
    private void Start()
    {
        enemyList = new List<EnemyHp>();
        transform.parent.localScale = scale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject.GetComponent<EnemyHp>());
        }
        if (collision.transform.CompareTag("Player")){
            playerHit = true;
        }
    }
    private void ExplodeDamage()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].Damaged(damage);
        }
        if (playerHit) GameManager.instance.player.damaged(damage);

    }
    private void ExplodeEnd()
    {
        Destroy(transform.parent.gameObject);
    }
    public void SetScale(int scale)
    {
        this.scale = new Vector2((float)scale * 0.5f, (float)scale * 0.5f);
    }
}
