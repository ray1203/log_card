using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BatCtrl : MonoBehaviour
{
    private int maxHp = 20;
    private int damage = 10;
    private float moveSpeed = 3f;
    private float maxMoveSpeed = 3f;
    private float attackDist = 3f;
    private float attackCool = 3f;
    private float attackTimer = 3f;
    private PlayerCtrl player;
    private SpriteRenderer sprite;
    private Animator animator;
    public bool attack = false;
    private GameObject attackCol;
    private bool enterPlayer = false;
    void Start()
    {
        player = GameManager.instance.player;
        sprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        attackCol = this.transform.Find("AttackCol").gameObject;
        attackCol.GetComponent<EnemyAttackCol>().damage = damage;
        attackCol.SetActive(false);
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
        animator.SetTrigger("Attack");
        moveSpeed = 7f;
    }
    public void Attack()
    {
        //Debug.Log("Attack");
        attack = true;
        attackCol.SetActive(true);
        animator.ResetTrigger("Attack");

    }
    public void AttackEnd()
    {
        //Debug.Log("AttackEnd");
        attack = false;
        attackCol.SetActive(false);
        moveSpeed = maxMoveSpeed;
        attackTimer = 0f;
    }
}