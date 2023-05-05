using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int FirstLevelIndex = 1;
    public void NewGame() {
        SceneManager.LoadScene(FirstLevelIndex);
    }

    public void Continue() {
        Debug.Log("Continue not implemented yet...");
        throw new System.Exception("Continue logic is not implemented yet");
    }

    public void Options() {
        Debug.Log("Continue not implemented yet...");
        throw new System.Exception("Continue logic is not implemented yet");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
