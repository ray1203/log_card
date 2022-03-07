using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int hp;
    public void Damaged(int amount)
    {
        //Debug.Log(gameObject.name + " damaged "+amount);
        hp -= amount;
        if (hp <= 0) gameObject.GetComponent<Animator>().SetTrigger("Death");
    }
    public void Die()
    {
        GameManager.instance.myCard.AddCard(GameManager.instance.deck.Draw());
        Debug.Log("enemy died");
        //GameManager.instance.player.gameObject.GetComponent<PlayerAttackCol>().removeEnemy(this);
        Destroy(this.gameObject);
    }
}
