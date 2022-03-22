using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int hp;
    public int dropPer = 1;//1 == 10(%)
    public void Damaged(int amount)
    {
        //Debug.Log(gameObject.name + " damaged "+amount);
        hp -= amount;
        if (hp <= 0) {
            Animator animator;
            if (gameObject.TryGetComponent<Animator>(out animator))
                gameObject.GetComponent<Animator>().SetTrigger("Death");
            else Die();
        }
    }
    public void Die()
    {
        GameManager.instance.myCard.AddCard(GameManager.instance.deck.Draw());
        Debug.Log("enemy died");
        int r = Random.Range(1, 11);
        if (r <= dropPer) GameManager.instance.DropCard(this.transform.position);
        //GameManager.instance.player.gameObject.GetComponent<PlayerAttackCol>().removeEnemy(this);
        Destroy(this.gameObject);
    }
}
