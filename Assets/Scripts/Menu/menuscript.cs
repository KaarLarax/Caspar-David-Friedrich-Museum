using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("home");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void gotoDocumentation()
    {
        Application.OpenURL("https://example.com/documentation");
    }
}
