using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject wonPanel, portalPlacer;

    public void Win() {
        wonPanel.SetActive(true);
        portalPlacer.GetComponent<PortalPlacement>().allowedToPlace = false;
    }

    public void Lose() {

    }
}
