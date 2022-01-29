using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCtrl : MonoBehaviour
{
    private int maxHp = 100;
    private int damage = 10;
    private float moveSpeed = 3f;
    private float maxMoveSpeed = 3f;
    private int attackCounter = 0;
    private PlayerCtrl player;
    private SpriteRenderer sprite;
    private Animator animator;
    public bool attack1 = false, attack2 = false;
    private Vector2 attackPos;
    private EnemyAttackCol attackCol;
    private bool enterPlayer = false;
    void Start()
    {
        player = GameManager.instance.player;
        sprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        attackCol = this.transform.Find("AttackCol").GetComponent<EnemyAttackCol>();
        attackCol.GetComponent<EnemyAttackCol>().damage = damage;
        //attackCol.gameObject.SetActive(false);
        moveSpeed = maxMoveSpeed;
        gameObject.GetComponent<EnemyHp>().hp = maxHp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enterPlayer = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!enterPlayer&&!attack1&&!attack2)
            Move(player.transform.position);
        else if (attack1)
        {
            Move(attackPos);
        }
    }
    public void AddCounter()
    {
        animator.SetInteger("counter",attackCounter++);
    }
    void Move(Vector2 movePos)
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
        if (transform.position.x > player.transform.position.x) sprite.flipX = true;
        else sprite.flipX = false;
    }

    void Attack1Start()
    {
        attackCol.attackChance = true;

        attackPos = player.transform.position;
        //Debug.Log("AttackStart");
        //animator.SetTrigger("Attack");
        moveSpeed = 0;
        attackCounter = 0;
        animator.SetInteger("counter", attackCounter);
       //attackCol.gameObject.SetActive(true);
        attack1 = true;
    }
    public void Attack1End()
    {
        attackCol.attackChance = false;
        //Debug.Log("AttackEnd");
        //attackCol.gameObject.SetActive(false);
        attack1 = false;
    }
    void Attack2Start()
    {
        attackCol.attackChance = true;
        attack2 = true;
        //Debug.Log("AttackStart");
        //animator.SetTrigger("Attack");
        //attackCol.gameObject.SetActive(true);
    }
    public void Attack2End()
    {
        attackCol.attackChance = false;
        //Debug.Log("AttackEnd");
        attack2 = false;
        //attackCol.gameObject.SetActive(false);
        moveSpeed = maxMoveSpeed;
    }
}
