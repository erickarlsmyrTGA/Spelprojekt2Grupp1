using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void SelectLevel()
    {

    }

    public void Credits()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Scene: QUIT");
        Application.Quit();
    }
}
