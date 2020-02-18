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
        wonPanel.SetActive(true);
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        time = timer.GetComponent<Timer>().time;
        Destroy(timer);
        timeShown.GetComponent<TextMeshProUGUI>().text = time.ToString() + "s";
        Camera.main.enabled = false;
        secondCamera.gameObject.SetActive(true);
        secondCamera.enabled = true;
    }

    public void Lose() {
        lostPanel.SetActive(true);
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        Destroy(timer);
        Camera.main.enabled = false;
        secondCamera.gameObject.SetActive(true);
        secondCamera.enabled = true;
    }
}
