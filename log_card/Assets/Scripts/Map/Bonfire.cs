using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private float timer = 0f;
    private bool isPlayer = false;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.3f&&isPlayer)
        {
            timer = 0f;
            GameManager.instance.player.Heal(1);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
