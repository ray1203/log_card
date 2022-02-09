using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
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

    private float checkCool = 3f;
    private float checkTimer = 3f;
    private Vector2[] moveRoot;
    private int moveIdx = 0;
    private int moveLen = 0;
    public int moveFlag = 2;//0: 기본 이동, 1: 장애물 피해서 이동 ,2: 무작위 이동

    public Vector2 nextRoot;//디버그용 
    private int recognizeRange = 10;
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
        moveRoot = new Vector2[1000];
        if (Vector2.Distance(transform.position, player.transform.position) <= recognizeRange && !GameManager.instance.checkWall(transform.position, player.transform.position))
        {
            moveFlag = 0;
        }
        else { 
            moveFlag = 2;
            CheckRoot(2);
        }
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
        nextRoot = moveRoot[moveIdx];
        checkTimer += Time.deltaTime;
        if (moveFlag!=2 && checkTimer >= checkCool)
        {
            if (GameManager.instance.checkWall(transform.position, player.transform.position))
            {
                checkTimer = 0;
                CheckRoot(1);
            }
            else
            {
                moveFlag = 0;
            }
        } else if (GameManager.instance.checkWall(transform.position, player.transform.position)&&moveFlag==0)
        {
            CheckRoot(1);
        }
        if (!enterPlayer && !attack1 && !attack2)
        {
            if (moveFlag == 0)
            {
                Move(player.transform.position);
            }
            else if (moveFlag == 1 || moveFlag == 2)
            {
                try
                {
                    if (moveLen == 0 || moveLen <= moveIdx)
                    {

                    }
                    else
                    {
                        
                        Move(moveRoot[moveIdx]);
                        if (Vector2.Distance(transform.position, moveRoot[moveIdx]) <= 0.1f)
                        {
                            moveIdx++;
                            if (moveLen <= moveIdx&&moveFlag==1)
                            {
                                moveFlag = 0;
                                moveIdx = 0;
                                CheckRoot(1);
                                checkTimer = 0;
                                moveLen = 0;
                            }else if(moveFlag == 2)
                            {
                                if (Vector2.Distance(transform.position, player.transform.position) <= recognizeRange && !GameManager.instance.checkWall(transform.position, player.transform.position))
                                {
                                    moveFlag = 0;
                                    checkTimer = checkCool;
                                }
                                else if (moveLen <= moveIdx)
                                {
                                    moveIdx = 0;
                                    CheckRoot(2);
                                }
                            }
                        }
                    }
                }
                catch (Exception e) { Debug.Log(e); }
            }
        }
        else if (attack1)
        {
            Move(attackPos);
        }
    }
    private void CheckRoot(int flag)
    {
        moveFlag = -1;
        Vector2[] newList;
        if (flag == 1) newList = (Vector2[])GameManager.instance.moveAlgorithm.FindRoot(transform.position, player.transform.position).Clone();
        else if (flag == 2) newList = (Vector2[])GameManager.instance.moveAlgorithm.FindRandomRoot(transform.position).Clone();
        else return;
        //moveRoot.Clear();
        //moveRoot.Capacity = newList.Count + 1;
            String log = "";
            for (int i = 0; i < newList.Length; i++) { 
                if (newList[i] == new Vector2(0, 0)) break; 
                moveRoot[i] = newList[i];
                log += moveRoot[i];
            }
            Debug.Log(log);
            //newList.Clear();
            moveLen = newList.Length;
            Debug.Log(transform.position + " " + player.transform.position + " " + moveRoot);
            moveIdx = 0;
            moveFlag = flag;
        
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
