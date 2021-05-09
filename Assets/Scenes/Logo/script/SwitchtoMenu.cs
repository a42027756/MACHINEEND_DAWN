using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchtoMenu : MonoBehaviour
{
    public void Switch()
    {
        SceneManager.LoadScene("Prelogue");
    }
}
