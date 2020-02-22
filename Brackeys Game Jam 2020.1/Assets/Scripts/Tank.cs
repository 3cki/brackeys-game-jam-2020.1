using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public AudioClip start, loop, explosionSound;

    public GameObject track;
    public GameObject turret;
    public GameObject controller;
    public GameObject leftTrail, rightTrail;
    public GameObject explosion, fire;
    public float speed;

    private List<Vector3> waypoints;
    private int currentWaypoint;
    public Rigidbody rb;
    private GameObject leftPortal, rightPortal, lastPortal;
    private bool teleportable = true;
    private bool allowedToSwitchTeleportable = true;
    private Vector3 portalCenter;
    public bool allowedToDrive = true;
    public bool finished = false;

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
            waypoints.Add(new Vector3(child.position.x, child.position.y, child.position.z));
        }

        // get rigidbody
        rb = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().enabled = true;
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
        targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);
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
            if (other.gameObject.name == "Waypoint (" + (currentWaypoint + 1).ToString() + ")" && other.gameObject.transform.parent == track.transform) {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Count)
                    currentWaypoint = 0;
            }
        }

        // entering last waypoint
        if (other.gameObject.tag == "FinalWaypoint" && currentWaypoint == waypoints.Count - 1) {
            /*controller.GetComponent<GameController>().Win();
            GetComponent<Animator>().enabled = false;
            allowedToDrive = false;*/
            finished = true;
            allowedToDrive = false;
        }

        // entering portal
        if (other.gameObject.tag == "Portal" && teleportable && !finished) {
            Camera.main.GetComponent<AudioSource>().Play();
            if (other.gameObject.name == "LeftPortal") {
                // teleport to right portal
                if (rightPortal.activeSelf) {
                    // disable trail
                    leftTrail.GetComponent<TrailRenderer>().emitting = false;
                    rightTrail.GetComponent<TrailRenderer>().emitting = false;

                    lastPortal = leftPortal;
                    allowedToDrive = false;
                    StartCoroutine(Teleport(rightPortal.transform.position));

                    teleportable = false;
                }
            } else {
                // teleport to left portal
                if (leftPortal.activeSelf) {
                    // disable trail
                    leftTrail.GetComponent<TrailRenderer>().emitting = false;
                    rightTrail.GetComponent<TrailRenderer>().emitting = false;

                    lastPortal = rightPortal;
                    allowedToDrive = false;
                    StartCoroutine(Teleport(leftPortal.transform.position));

                    teleportable = false;
                }
            }
        }
    }

    public void StartAnim() {
        allowedToSwitchTeleportable = false;
    }

    public void FinishedAnim() {
        GetComponent<Animator>().SetBool("Teleporting", false);
        allowedToSwitchTeleportable = true;
    }

    IEnumerator Teleport(Vector3 teleportPosition) {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().SetBool("Teleporting", true);
        yield return new WaitForSeconds(0.2f);
        transform.position = teleportPosition + portalCenter;
        allowedToDrive = true;
        Camera.main.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        allowedToSwitchTeleportable = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Lethal" || collision.gameObject.tag == "Tank") {
            GetComponent<AudioSource>().clip = explosionSound;
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
            controller.GetComponent<GameController>().Lose();
            allowedToDrive = false;
            explosion.SetActive(true);
            fire.SetActive(true);
            GetComponent<Animator>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "LeftPortal" && lastPortal == rightPortal && allowedToSwitchTeleportable) {
            teleportable = true;
            leftTrail.GetComponent<TrailRenderer>().emitting = true;
            rightTrail.GetComponent<TrailRenderer>().emitting = true;
        }
        if (other.gameObject.name == "RightPortal" && lastPortal == leftPortal && allowedToSwitchTeleportable) {
            teleportable = true;
            leftTrail.GetComponent<TrailRenderer>().emitting = true;
            rightTrail.GetComponent<TrailRenderer>().emitting = true;
        }
    }
}
