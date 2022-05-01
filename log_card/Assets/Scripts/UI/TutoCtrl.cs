using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutoCtrl : MonoBehaviour
{

    public Sprite[] sprites;
    public Image image;
    int c = 0;
    private void Start()
    {
        image.sprite = sprites[0];
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            c++;
            if (c >= 7)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                image.sprite = sprites[c];
            }
        }else if (Input.GetMouseButtonDown(1))
        {
            c--;
            if (c < 0) c = 0;
            image.sprite = sprites[c];
        }
    }
}
