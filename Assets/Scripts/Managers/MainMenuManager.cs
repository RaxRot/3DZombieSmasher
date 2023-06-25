using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(TagManager.SELECT_CHARACTER_SCENE_NAME);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
