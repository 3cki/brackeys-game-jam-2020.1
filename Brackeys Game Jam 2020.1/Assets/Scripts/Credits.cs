using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void LoadMenu() {
        StartCoroutine(Menu());
    }

    IEnumerator Menu() {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
    }
}
