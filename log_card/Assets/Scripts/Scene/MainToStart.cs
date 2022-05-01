using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainToStart : MonoBehaviour
{
    void Update()
    {
        
    }
    public void StartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void TutoBtn()
    {
        SceneManager.LoadScene("TutoScene");
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
}
