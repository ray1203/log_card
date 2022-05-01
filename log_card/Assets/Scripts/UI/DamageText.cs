using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float floatingSpeed = 1f;
    private float timer = 0.7f;
    void Update()
    {
        this.transform.position = new Vector2(transform.position.x, transform.position.y+floatingSpeed*Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0f) Destroy(this.gameObject);
    }
    
}
