using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject wonPanel, lostPanel, portalPlacer, timer, timeShown, tank, pauseScreen;
    public Camera secondCamera;

    private float time;
    private bool pausable = true;

    private void Start() {
        Pause();
    }

    void Pause() {
        tank.GetComponent<Tank>().allowedToDrive = !tank.GetComponent<Tank>().allowedToDrive;
        timer.GetComponent<Timer>().timing = !timer.GetComponent<Timer>().timing;
        tank.GetComponent<Tank>().rb.velocity = Vector3.zero;
        pauseScreen.SetActive(true);
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && pausable) {
            tank.GetComponent<Tank>().allowedToDrive = !tank.GetComponent<Tank>().allowedToDrive;
            timer.GetComponent<Timer>().timing = !timer.GetComponent<Timer>().timing;
            pauseScreen.SetActive(false);
            if (!tank.GetComponent<Tank>().allowedToDrive) {
                tank.GetComponent<Tank>().rb.velocity = Vector3.zero;
                pauseScreen.SetActive(true);
            }
            portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = true;
            pausable = false;
        }
    }

    public void Win() {
        pausable = false;
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        time = timer.GetComponent<Timer>().time;
        Destroy(timer);
        timeShown.GetComponent<TextMeshProUGUI>().text = time.ToString() + "s";
        StartCoroutine(SwitchCameraWin());
    }

    public void Lose() {
        pausable = false;
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
