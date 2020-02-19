using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private bool fadeout = false;

    void Update() { 
        if (fadeout)
            Camera.main.GetComponent<AudioSource>().volume = Mathf.Lerp(Camera.main.GetComponent<AudioSource>().volume, 0, 0.005f);
    }

    public void LoadMenu() {
        fadeout = true;
        StartCoroutine(Menu());
    }

    IEnumerator Menu() {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
    }
}
