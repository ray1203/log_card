using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    public int hp;
    private float defence = 1f;
    public int dropPer = 1;//1 == 10(%)
    public bool attack = false;
    private Animator animator;
    public GameObject damageText;
    private void Start()
    {
        animator = null;
        gameObject.TryGetComponent<Animator>(out animator);
        damageText = GameManager.instance.damageText;
    }
    public void Damaged(int amount)
    {
        amount = (int)((float)amount * defence);
        GameObject newObject = Instantiate(damageText);
        newObject.transform.position = transform.position;
        newObject.transform.SetParent(GameManager.instance.canvas.transform);
        newObject.transform.localScale = new Vector2(1, 1);
        newObject.GetComponent<Text>().text = amount + "";

        //Debug.Log(gameObject.name + " damaged "+amount);
        if (hp > 0)
        {
            hp -= amount;
            if (animator != null&&!attack)
                animator.SetTrigger("Hit");
        }
        if (hp <= 0)
        {
            if (animator != null)
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
    public void ChangeDefence(float value)
    {
        defence = value;
    }
}
