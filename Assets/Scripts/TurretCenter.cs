using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurretCenter : MonoBehaviour
{
    public Transform leftController;
    public Transform rightController;
    public Transform turretPivotPoint;

    private Vector3 middle;
    public Quaternion forwardNumber;
    public Quaternion downNumber;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        middle = (leftController.position + rightController.position) / 2.0f;
        transform.position = middle;
        transform.forward = 0.5f*((leftController.up * -1) + (rightController.up * -1));
        turretPivotPoint.forward = transform.forward;
    }
}
