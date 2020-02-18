using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {

    }

    public void NextLevel() {

    }
}
