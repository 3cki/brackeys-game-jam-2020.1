using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject wonPanel, lostPanel, portalPlacer, timer, timeShown;

    private float time;

    public void Win() {
        wonPanel.SetActive(true);
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        time = timer.GetComponent<Timer>().time;
        Destroy(timer);
        timeShown.GetComponent<TextMeshProUGUI>().text = time.ToString() + "s";
    }

    public void Lose() {
        lostPanel.SetActive(true);
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        Destroy(timer);
    }
}
