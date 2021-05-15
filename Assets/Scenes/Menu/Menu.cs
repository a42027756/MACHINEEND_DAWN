using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void Switch2Game()
    {
        SceneManager.LoadScene("Prelogue");
    }

    public void Switch2Map()
    {
        SceneManager.LoadScene("LoadScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
