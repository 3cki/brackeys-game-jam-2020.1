using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject track;
    public GameObject turret;
    public GameObject controller;
    public GameObject leftTrail, rightTrail;
    public float speed;

    private List<Vector3> waypoints;
    private int currentWaypoint;
    private Rigidbody rb;
    private GameObject leftPortal, rightPortal, lastPortal;
    private bool teleportable = true;
    private Vector3 portalCenter;
    private bool allowedToDrive = true;

    private void Awake() {
        // get portals
        leftPortal = GameObject.Find("LeftPortal");
        rightPortal = GameObject.Find("RightPortal");
        portalCenter = new Vector3(0, 0, 5);
    }

    void Start()
    {
        // create list of waypoints the tank has to pass
        if (track == null)
            track = GameObject.Find("Track");

        waypoints = new List<Vector3>();

        foreach (Transform child in track.transform) {
            waypoints.Add(new Vector3(child.position.x, transform.position.y, child.position.z));
        }

        // get rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (allowedToDrive) {
            LookAtWaypoint();
            MoveTowardsWaypoint();
            TurretLookAtWaypoint();
        }
    }

    void LookAtWaypoint() {
        Vector3 targetDirection = waypoints[currentWaypoint] - transform.position;
        float singleStep = speed * Time.deltaTime / 10;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void TurretLookAtWaypoint() {
        Vector3 targetDirection = waypoints[currentWaypoint] - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, singleStep);
        turret.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void MoveTowardsWaypoint() {
        rb.velocity = (transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other) {
        // entering normal waypoint
        if (other.gameObject.tag == "Waypoint") {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
                currentWaypoint = 0;
        }

        // entering last waypoint
        if (other.gameObject.tag == "FinalWaypoint") {
            controller.GetComponent<GameController>().Win();
            allowedToDrive = false;
        }

        // entering portal
        if (other.gameObject.tag == "Portal" && teleportable) {
            // disable trail
            leftTrail.GetComponent<TrailRenderer>().emitting = false;
            rightTrail.GetComponent<TrailRenderer>().emitting = false;

            if (other.gameObject.name == "LeftPortal") {
                // teleport to right portal
                lastPortal = leftPortal;
                allowedToDrive = false;
                StartCoroutine(Teleport(rightPortal.transform.position));
            } else {
                // teleport to left portal
                lastPortal = rightPortal;
                allowedToDrive = false;
                StartCoroutine(Teleport(leftPortal.transform.position));
            }
            teleportable = false;
        }
    }

    IEnumerator Teleport(Vector3 teleportPosition) {
        yield return new WaitForSeconds(0.1f);
        transform.position = teleportPosition + portalCenter;
        allowedToDrive = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Lethal") {
            controller.GetComponent<GameController>().Lose();
            allowedToDrive = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "LeftPortal" && lastPortal == rightPortal) {
            teleportable = true;
            leftTrail.GetComponent<TrailRenderer>().emitting = true;
            rightTrail.GetComponent<TrailRenderer>().emitting = true;
        }
        if (other.gameObject.name == "RightPortal" && lastPortal == leftPortal) {
            teleportable = true;
            leftTrail.GetComponent<TrailRenderer>().emitting = true;
            rightTrail.GetComponent<TrailRenderer>().emitting = true;
        }
    }
}
