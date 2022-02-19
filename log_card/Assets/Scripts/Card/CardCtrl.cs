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
        else if (control && !card.targeting)
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        Debug.Log(Input.mousePosition);
    }
}
