using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    void Start() {
        Time.timeScale = 1;
    }

    public void StartGameButton() {
        SceneManager.LoadScene(1);
    }

    public void QuitGameButton() {
        Application.Quit();
    }

}
