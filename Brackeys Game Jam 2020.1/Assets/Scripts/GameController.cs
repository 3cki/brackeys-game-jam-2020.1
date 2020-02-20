using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject wonPanel, lostPanel, portalPlacer, timer, timeShown, tank, pauseScreen;
    public Camera secondCamera;
    public bool multipleTanks = false;
    public List<GameObject> tanks;

    private float time;
    private bool pausable = true;

    private void Start() {
        Pause();
    }

    void Pause() {
        timer.GetComponent<Timer>().timing = !timer.GetComponent<Timer>().timing;
        pauseScreen.SetActive(true);
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;

        if (!multipleTanks) {
            tank.GetComponent<Tank>().allowedToDrive = !tank.GetComponent<Tank>().allowedToDrive;
            tank.GetComponent<Tank>().rb.velocity = Vector3.zero;
        } else {
            foreach (GameObject mtank in tanks) {
                mtank.GetComponent<Tank>().allowedToDrive = !mtank.GetComponent<Tank>().allowedToDrive;
                mtank.GetComponent<Tank>().rb.velocity = Vector3.zero;
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && pausable) {
            timer.GetComponent<Timer>().timing = !timer.GetComponent<Timer>().timing;
            StartCoroutine(SwitchTankSound());
            pauseScreen.SetActive(false);
            portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = true;
            pausable = false;

            if (!multipleTanks) {
                tank.GetComponent<AudioSource>().clip = tank.GetComponent<Tank>().start;
                tank.GetComponent<AudioSource>().loop = false;
                tank.GetComponent<AudioSource>().Play();
                tank.GetComponent<Tank>().allowedToDrive = !tank.GetComponent<Tank>().allowedToDrive;
                if (!tank.GetComponent<Tank>().allowedToDrive) {
                    tank.GetComponent<Tank>().rb.velocity = Vector3.zero;
                    pauseScreen.SetActive(true);
                }
            } else {
                foreach (GameObject mtank in tanks) {
                    mtank.GetComponent<AudioSource>().clip = mtank.GetComponent<Tank>().start;
                    mtank.GetComponent<AudioSource>().loop = false;
                    mtank.GetComponent<AudioSource>().Play();
                    mtank.GetComponent<Tank>().allowedToDrive = !mtank.GetComponent<Tank>().allowedToDrive;
                }
            }
        }

        if (!multipleTanks) {
            if (tank.GetComponent<Tank>().finished == true) {
                Win();
                tank.GetComponent<Animator>().enabled = false;
            }
        } else {
            int wincounter = 0;
            foreach (GameObject mtank in tanks) {
                if (mtank.GetComponent<Tank>().finished == true) {
                    wincounter++;
                }
            }
            if (wincounter == tanks.Count) {
                Win();
                foreach (GameObject mtank in tanks) {
                    mtank.GetComponent<Animator>().enabled = false;
                }
            }
        }
    }

    IEnumerator SwitchTankSound() {
        yield return new WaitForSeconds(0.9f);
        if (!multipleTanks) {
            tank.GetComponent<AudioSource>().clip = tank.GetComponent<Tank>().loop;
            tank.GetComponent<AudioSource>().loop = true;
            tank.GetComponent<AudioSource>().Play();
        } else {
            foreach (GameObject mtank in tanks) {
                mtank.GetComponent<AudioSource>().clip = mtank.GetComponent<Tank>().loop;
                mtank.GetComponent<AudioSource>().loop = true;
                mtank.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void Win() {
        pausable = false;
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        time = timer.GetComponent<Timer>().time;
        Destroy(timer);
        timeShown.GetComponent<TextMeshProUGUI>().text = time.ToString() + "s";
        StartCoroutine(SwitchCameraWin());

        if (multipleTanks) {
            foreach (GameObject mtank in tanks) {
                mtank.GetComponent<Animator>().enabled = false;
                mtank.GetComponent<Tank>().allowedToDrive = false;
            }
        }
    }

    public void Lose() {
        pausable = false;
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
        Destroy(timer);
        StartCoroutine(SwitchCameraLose());
        if (multipleTanks) {
            foreach (GameObject mtank in tanks) {
                mtank.GetComponent<Animator>().enabled = false;
                mtank.GetComponent<Tank>().allowedToDrive = false;
            }
        }
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
