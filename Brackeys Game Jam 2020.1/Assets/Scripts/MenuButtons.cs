using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Level 1");
    }

    public void Credits() {

    }

    public void QuitGame() {
        Application.Quit();
    }
}
