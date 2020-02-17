using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject track;
    public GameObject turret;
    public float speed;

    private List<Vector3> waypoints;
    private int currentWaypoint;
    private Rigidbody rb;

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
        LookAtWaypoint();
        MoveTowardsWaypoint();
        TurretLookAtWaypoint();
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
        if (other.gameObject.tag == "Waypoint") {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
                currentWaypoint = 0;
        }
    }
}
