using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("player damaged " + damage + " by" + transform.parent.name);
            collision.GetComponent<PlayerCtrl>().damaged(damage);
        }
    }
}
