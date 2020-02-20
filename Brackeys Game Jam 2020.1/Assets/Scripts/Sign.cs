using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject tank;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(tank.transform.position.x, offset.y, tank.transform.position.z);
        transform.position = newPos;
    }
}
