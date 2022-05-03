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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHp enemy = collision.gameObject.GetComponent<EnemyHp>();
            if (!enemyList.Contains(enemy))
                enemyList.Add(enemy);
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
            
            GameManager.instance.player.AddMp(1);
            enemyList[i].Damaged((int)((float)damage*BuffManager.instance.GetValue(BuffStat.damage)));
        }
    }
    public void RemoveEnemy(EnemyHp enemyHp)
    {
        enemyList.Remove(enemyHp);
    }
}
