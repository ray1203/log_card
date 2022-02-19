using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCtrl : MonoBehaviour
{
    public Vector2 pos;
    public Card card;
    public bool control = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!control)
            transform.localPosition = pos;
        else
        {
            if (!card.targeting)
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(1))
            {
                Time.timeScale = 1f;
                control = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Time.timeScale = 1f;
                control = false;
                card.func();
                GameManager.instance.myCard.cardObjects.Remove(this);
                Destroy(this.gameObject);
            }
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        Debug.Log(Input.mousePosition);
    }

}
