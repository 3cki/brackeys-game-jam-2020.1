using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject wonPanel, lostPanel, portalPlacer, timer, timeShown;
    public Camera secondCamera;

    private float time;

    public void Win() {
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        time = timer.GetComponent<Timer>().time;
        Destroy(timer);
        timeShown.GetComponent<TextMeshProUGUI>().text = time.ToString() + "s";
        StartCoroutine(SwitchCameraWin());
    }

    public void Lose() {
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        Destroy(timer);
        StartCoroutine(SwitchCameraLose());
    }

    IEnumerator SwitchCameraWin() {
        yield return new WaitForSeconds(1f);
        Camera.main.enabled = false;
        secondCamera.gameObject.SetActive(true);
        secondCamera.enabled = true;
        wonPanel.SetActive(true);
    }

    IEnumerator SwitchCameraLose() {
        yield return new WaitForSeconds(1f);
        Camera.main.enabled = false;
        secondCamera.gameObject.SetActive(true);
        secondCamera.enabled = true;
        lostPanel.SetActive(true);
    }
}
