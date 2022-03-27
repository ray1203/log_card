using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabCtrl : MonoBehaviour
{
    public GameObject gameOverTab, pauseTab,victoryTab;
    private bool paused = false;
    private bool myCardIsActive;
    public void Resume()
    {
        GameManager.instance.myCard.myCard.SetActive(myCardIsActive);
        paused = false;
        Time.timeScale = 1f;
        pauseTab.SetActive(false);
    }
    public void Pause()
    {
        myCardIsActive = GameManager.instance.myCard.myCard.activeSelf;
        GameManager.instance.myCard.myCard.SetActive(false);
        paused = true;
        Time.timeScale = 0f;
        pauseTab.SetActive(true);
    }
    public void GameOver()
    {
        GameManager.instance.myCard.myCard.SetActive(false);
        Time.timeScale = 0f;
        gameOverTab.SetActive(true);
    }
    public void Victory()
    {
        GameManager.instance.myCard.myCard.SetActive(false);
        Time.timeScale = 0f;
        victoryTab.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                Pause();
            else
                Resume();
        }
    }
}
