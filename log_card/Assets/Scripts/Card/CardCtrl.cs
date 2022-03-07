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
            else
            {
                GameManager.instance.crosshair.SetActive(true);
                GameManager.instance.crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)-new Vector3(0,0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
                transform.position = GameManager.instance.player.transform.position + new Vector3(8, 0, 0);
            }
            if (Input.GetMouseButtonDown(1))//취소
            {
                GameManager.instance.crosshair.SetActive(false);
                Time.timeScale = 1f;
                control = false;
            }
            if (Input.GetMouseButtonUp(0))//발동
            {
                GameManager.instance.crosshair.SetActive(false);
                Time.timeScale = 1f;
                control = false;
                if (GameManager.instance.player.UseMp(card.cost)) {
                    card.func();
                    GameManager.instance.myCard.cardObjects.Remove(this);
                    Destroy(this.gameObject);
                }
                else
                {

                }
            }
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
    }

}
