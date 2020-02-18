using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlacement : MonoBehaviour
{
    public LayerMask clickMask;
    public GameObject portalPreview, leftPortal, rightPortal;
    public bool allowedToPlace = true;

    private Vector3 clickPosition;
    private Vector3 portalCenter;

    private void Start() {
        leftPortal.SetActive(false);
        rightPortal.SetActive(false);
        portalCenter = new Vector3(0, 0, -5);
    }

    private void Update() {
        if (allowedToPlace) {
            positionPortalPreview();
            placePortal();
        } else {
            portalPreview.SetActive(false);
        }
    }

    void positionPortalPreview() {
        clickPosition = -Vector3.one;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 300f, clickMask)) {
            clickPosition = hit.point;
        }

        portalPreview.transform.position = clickPosition + portalCenter;
    }

    void placePortal() {
        if (Input.GetMouseButtonDown(0)) {
            // place left portal
            leftPortal.SetActive(true);
            leftPortal.transform.position = clickPosition + portalCenter;
        }

        if (Input.GetMouseButtonDown(1)) {
            // place right portal
            rightPortal.SetActive(true);
            rightPortal.transform.position = clickPosition + portalCenter;
        }
    }
}
