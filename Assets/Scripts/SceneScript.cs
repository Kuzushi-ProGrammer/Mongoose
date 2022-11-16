using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ActivateSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ExitSettingsMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
