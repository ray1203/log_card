using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class GolemCtrl : MonoBehaviour
{
    private float moveSpeed = 2f;
    private SpriteRenderer sprite;
    private Animator animator;
    public RuntimeAnimatorController golem_a,golem_b,golem_reinforced;
    public EnemyAttackCol attackCol1, attackCol2, attackCol3, reinforcedAttackCol1, reinforcedAttackCol2;
    private int maxHp;
    private EnemyHp hp;
    private Transform playerTransform;
    private Vector3 scale;
    private bool attack = false, ability = false, reinforced = false;
    private float attack2Cool = 3f, attack2Timer = 10f;
    private float abilityHp;
    private float abilityTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        hp = this.GetComponent<EnemyHp>();
        playerTransform = GameManager.instance.player.transform;
        scale = this.transform.localScale;
        maxHp = hp.hp;
        abilityHp = maxHp - maxHp / 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attack)
        {
            Move();
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if(Vector2.Distance(playerTransform.position, transform.position) <= 2f&&attack2Cool<=attack2Timer)
        {
            attack2Timer = 0f;
            if (!reinforced)
                Attack2Start();
            else
                ReinforcedAttack2Start();
        }
        attack2Timer += Time.deltaTime;

        if (hp.hp <= abilityHp && !ability && !attack&&!reinforced)
        {
            Debug.Log("Abilstart");
            AbilityStart();
        }
        if (ability)
        {
            abilityTimer += Time.deltaTime;
            if(hp.hp < abilityHp - 50)
            {
                abilityHp -= maxHp / 4;
                abilityTimer = 0f;
                Debug.Log("변신실패");
                Attack1Start();
            }
            else if (abilityTimer >= 5f&&hp.hp >= abilityHp - 50)
            {
                abilityHp -= maxHp / 4;
                abilityTimer = 0f;
                ReinforceStart();
                Debug.Log("변신");
                
            }
        }
    }

    public void Move()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) >= 1f)
        {
            animator.SetBool("Run", true);
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            
        }
        else
            animator.SetBool("Run", false);
        if (playerTransform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void AbilityStart()
    {
        abilityTimer = 0f;
        ability = true;
        animator.SetTrigger("Ability");
        hp.ChangeDefence(0.5f);
    }
    public void AbillityEnd()
    {
        animator.runtimeAnimatorController = golem_b;
    }
    public void Attack1Start()
    {
        attack = true;
        animator.SetTrigger("Attack");
    }
    public void Attack1ColStart()
    {
        attackCol1.gameObject.SetActive(true);
        attackCol1.attackChance = true;
    }
    public void Attack1ColEnd()
    {
        attackCol1.gameObject.SetActive(false);
        attackCol1.attackChance = false;
    }
    public void Attack1End()
    {
        attack = false;
        ability = false;
        animator.runtimeAnimatorController = golem_a;
    }
    public void GolemEndAbility()
    {
        animator.runtimeAnimatorController = golem_a;
    }

    public void Attack2Start()
    {
        attack = true;
        animator.SetTrigger("Attack 2");
    }
    public void Attack2ColStart()
    {
        attackCol2.gameObject.SetActive(true);
        attackCol2.attackChance = true;
    }
    public void Attack2ColEnd()
    {
        attackCol2.gameObject.SetActive(false);
        attackCol2.attackChance = false;
    }
    public void Attack2End()
    {
        Attack3Start();
    }
    public void Attack3Start()
    {
        attack = true;
        animator.SetTrigger("Attack 3");
    }
    public void Attack3ColStart()
    {
        attackCol3.gameObject.SetActive(true);
        attackCol3.attackChance = true;
    }
    public void Attack3ColEnd()
    {
        attackCol3.gameObject.SetActive(false);
        attackCol3.attackChance = false;
    }
    public void Attack3End()
    {
        attack = false;
    }
    public void ReinforceStart()
    {
        animator.SetTrigger("Ability");
        reinforced = true;
    }
    public void Reinforce()
    {
        ability = false;
        attack = false;
        animator.runtimeAnimatorController = golem_reinforced;
    }

    public void ReinforcedAttack2Start()
    {
        attack = true;
        animator.SetTrigger("Attack 2");
    }
    public void ReinforcedAttack2ColStart()
    {
        reinforcedAttackCol2.gameObject.SetActive(true);
        reinforcedAttackCol2.attackChance = true;
    }
    public void ReinforcedAttack2ColEnd()
    {
        reinforcedAttackCol2.gameObject.SetActive(false);
        reinforcedAttackCol2.attackChance = false;
    }
    public void ReinforcedAttack2End()
    {
        ReinforcedAttack1Start();
    }
    public void ReinforcedAttack1Start()
    {
        attack = true;
        animator.SetTrigger("Attack");
    }
    public void ReinforcedAttack1ColStart()
    {
        reinforcedAttackCol1.gameObject.SetActive(true);
        reinforcedAttackCol1.attackChance = true;
    }
    public void ReinforcedAttack1ColEnd()
    {
        reinforcedAttackCol1.gameObject.SetActive(false);
        reinforcedAttackCol1.attackChance = false;
    }
    public void ReinforcedAttack1End()
    {
        attack = false;
    }
}
