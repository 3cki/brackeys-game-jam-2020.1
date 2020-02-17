using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlacement : MonoBehaviour
{
    public LayerMask clickMask;
    public GameObject portalPreview, leftPortal, rightPortal;

    private Vector3 clickPosition;

    private void Start() {
        leftPortal.SetActive(false);
        rightPortal.SetActive(false);
    }

    private void Update() {
        positionPortalPreview();
        placePortal();
    }

    void positionPortalPreview() {
        clickPosition = -Vector3.one;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 300f, clickMask)) {
            clickPosition = hit.point;
        }

        portalPreview.transform.position = clickPosition;
    }

    void placePortal() {
        if (Input.GetMouseButtonDown(0)) {
            // place left portal
            leftPortal.SetActive(true);
            leftPortal.transform.position = clickPosition;
        }

        if (Input.GetMouseButtonDown(1)) {
            // place right portal
            rightPortal.SetActive(true);
            rightPortal.transform.position = clickPosition;
        }
    }
}
