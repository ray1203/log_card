using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Animator animator;
    private int dir=2;//1: side 2: front 3: back
    private SpriteRenderer sprite;
    private int hp;
    private int maxHp = 100;
    private int damage = 100;//10
    private float attackRate = 0.5f;
    private Image hpBar;
    private GameObject attackCol;
    
    private void Start()
    {
        hp = maxHp;
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        hpBar = GameManager.instance.canvas.transform.Find("PlayerHpBar").GetComponent<Image>();
        attackCol = transform.Find("AttackCol").gameObject;
        attackCol.GetComponent<PlayerAttackCol>().damage = damage;
        attackCol.GetComponent<PlayerAttackCol>().attackRate = attackRate;
        attackCol.SetActive(false);
    }
    private void Update()
    {
        
        // 위, 아래로 움직이기
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            dir = 2;
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            attackCol.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            dir = 3;
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            attackCol.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        //좌 우
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
            dir = 1;
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            attackCol.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
            dir = 1;
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            attackCol.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetMouseButton(0))
        {
            attackCol.SetActive(true);
            animator.SetInteger("attack", dir);
        }
        else
        {
            attackCol.SetActive(false);
            animator.SetInteger("attack", -1);
            animator.SetInteger("idle", dir);
        }
    }
    public void damaged(int amount)
    {
        hp -= amount;
        hpBar.fillAmount = hpRatio();
    }
    public float hpRatio()
    {
        return (float)hp / (float)maxHp;
    }
    public void heal(int amount)
    {
        hp += amount;
        if (hp > maxHp) hp = maxHp;
        hpBar.fillAmount = hpRatio();

    }
}
