using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    public int damage;
    public bool isPlayer = false;
    public bool attackChance = false;
    private void LateUpdate()
    {
        if (isPlayer && attackChance)
        {
            Debug.Log("player damaged " + damage + " by" + transform.parent.name);
            GameManager.instance.player.Damaged(damage);
            attackChance = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayer = false;
        }
    }
}
