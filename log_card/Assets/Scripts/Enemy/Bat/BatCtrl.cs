using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BatCtrl : MonoBehaviour
{
    private int maxHp = 20;
    private int damage = 6;
    private float moveSpeed = 3f;
    private float maxMoveSpeed = 3f;
    private float attackDist = 3f;
    private float attackCool = 3f;
    private float attackTimer = 3f;
    private PlayerCtrl player;
    private SpriteRenderer sprite;
    private Animator animator;
    public bool attack = false;
    private EnemyAttackCol attackCol;
    private bool enterPlayer = false;
    private EnemyHp enemyHp;
    void Start()
    {
        player = GameManager.instance.player;
        sprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        attackCol = this.transform.Find("AttackCol").GetComponent<EnemyAttackCol>();
        attackCol.GetComponent<EnemyAttackCol>().damage = damage;
        //attackCol.gameObject.SetActive(false);
        moveSpeed = maxMoveSpeed;
        enemyHp = gameObject.GetComponent<EnemyHp>();
        enemyHp.hp = maxHp;

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
        attackTimer += Time.deltaTime;
        if (attackTimer>=attackCool && Vector2.Distance(gameObject.transform.position, player.transform.position) <= attackDist) AttackStart();
        if(!enterPlayer)
            Move();
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,moveSpeed*Time.deltaTime);
        if (transform.position.x > player.transform.position.x) sprite.flipX = true;
        else sprite.flipX = false;
    }
    
    void AttackStart()
    {
        attackTimer = 0f;
        //Debug.Log("AttackStart");
        enemyHp.attack = true;
        animator.SetTrigger("Attack");
        moveSpeed = 7f;
    }
    public void Attack()
    {
        attackCol.attackChance = true;
        //Debug.Log("Attack");
        attack = true;
        //attackCol.gameObject.SetActive(true);
        animator.ResetTrigger("Attack");

    }
    public void AttackEnd()
    {
        attackCol.attackChance = false;
        //Debug.Log("AttackEnd");
        attack = false;
        enemyHp.attack = false;
        //attackCol.gameObject.SetActive(false);
        moveSpeed = maxMoveSpeed;
        attackTimer = 0f;
    }
}
