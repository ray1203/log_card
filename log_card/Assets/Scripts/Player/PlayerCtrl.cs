using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Animator animator;
    private int dir=2;//1: side 2: front 3: back
    private SpriteRenderer sprite;
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        
        // 위, 아래로 움직이기
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            dir = 2;
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            dir = 3;
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
        }
        //좌 우
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
            dir = 1;
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
            dir = 1;
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("A");
            animator.SetInteger("attack", dir);
        }
        else
        {
            animator.SetInteger("attack", -1);
            animator.SetInteger("idle", dir);
        }
    }
}
