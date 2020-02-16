using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject track;

    private List<Vector2> waypoints;

    void Start()
    {
        // create list of waypoints the tank has to pass
        if (track == null)
            track = GameObject.Find("Track");

        waypoints = new List<Vector2>();

        foreach (Transform child in track.transform) {
            waypoints.Add(new Vector2(child.position.x, child.position.z));
        }
    }

    void Update()
    {
        
    }
}
