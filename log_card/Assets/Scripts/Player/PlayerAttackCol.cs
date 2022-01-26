using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCol : MonoBehaviour
{
    public int damage;
    public float attackRate;
    private float attackTimer = 0f;
    private List<EnemyHp> enemyList;
    private void Start()
    {
        enemyList = new List<EnemyHp>();
    }
    void Update()
    {
        Debug.Log("EnemyList: " + enemyList.Count);
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            attackTimer = 0f;
            Attack();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject.GetComponent<EnemyHp>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyList.Remove(collision.gameObject.GetComponent<EnemyHp>());
        }
    }
    private void Attack()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].Damaged(damage);
        }
    }
    public void removeEnemy(EnemyHp enemyHp)
    {
        enemyList.Remove(enemyHp);
    }
}
