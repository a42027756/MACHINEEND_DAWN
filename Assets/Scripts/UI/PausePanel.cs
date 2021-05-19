using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public GameObject pausepanel;
    public void Quit2Menu()
    {
        pausepanel.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void Quit2Desktop()
    {
        Application.Quit();
    }

    public void Back2Game()
    {
        pausepanel.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausepanel.SetActive(true);
    }
}
