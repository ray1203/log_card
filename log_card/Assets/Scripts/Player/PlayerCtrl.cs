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
    private int mp;
    private int damage = 100;//10
    private float attackRate = 0.5f;
    private Image hpBar;
    private Image mpBar;
    private Text mpText;
    private GameObject attackCol;
    
    private void Start()
    {
        mp = 3;
        hp = maxHp;
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        hpBar = GameManager.instance.canvas.transform.Find("PlayerHpBar").GetComponent<Image>();
        mpBar = GameManager.instance.canvas.transform.Find("PlayerMpBar").GetComponent<Image>();
        mpText = mpBar.transform.Find("Text").GetComponent<Text>();
        attackCol = transform.Find("AttackCol").gameObject;
        attackCol.GetComponent<PlayerAttackCol>().damage = damage;
        attackCol.GetComponent<PlayerAttackCol>().attackRate = attackRate;
        attackCol.SetActive(false);
    }
    private void Update()
    {
        if (BuffManager.instance.GetValue(BuffStat.stop) == 1)
        {
            // 위, 아래로 움직이기
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                dir = 2;
                transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime * (BuffManager.instance.GetValue(BuffStat.speed)), 0f));
                attackCol.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                dir = 3;
                transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime * (BuffManager.instance.GetValue(BuffStat.speed)), 0f));
                attackCol.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            //좌 우
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                sprite.flipX = false;
                dir = 1;
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime * (BuffManager.instance.GetValue(BuffStat.speed)), 0f, 0f));
                attackCol.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                sprite.flipX = true;
                dir = 1;
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime * (BuffManager.instance.GetValue(BuffStat.speed)), 0f, 0f));
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
    }
    public void Damaged(int amount)
    {
        amount -= (int)BuffManager.instance.GetValue(BuffStat.absolDef);
        Debug.Log((BuffManager.instance.GetValue(BuffStat.def)));
        amount = (int)((float)amount * (Mathf.Max(0,2f-(BuffManager.instance.GetValue(BuffStat.def)))));
        if (amount < 0) amount = 0;
        hp -= amount;
        hpBar.fillAmount = HpRatio();
    }
    public float HpRatio()
    {
        return (float)hp / (float)maxHp;
    }
    public void Heal(int amount)
    {
        hp += amount;
        if (hp > maxHp) hp = maxHp;
        hpBar.fillAmount = HpRatio();

    }
    public void AddMp(int amount)
    {
        mp += amount;
        if (mp > 10) mp = 10;
        mpBar.fillAmount = (float)mp / 10f;
        mpText.text = mp.ToString();
    }
    public bool UseMp(int amount)
    {
        if (mp < amount) return false;
        mp -= amount;
        mpBar.fillAmount = (float)mp / 10f;
        mpText.text = mp.ToString();
        return true;
    }
}
