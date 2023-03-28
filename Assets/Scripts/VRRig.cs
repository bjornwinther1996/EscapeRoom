using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{



    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;

        head.vrTarget = GameObject.Find("Main Camera").transform;
        leftHand.vrTarget = GameObject.Find("LeftHand Controller").transform;
        rightHand.vrTarget = GameObject.Find("RightHand Controller").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        if (Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized != Vector3.zero)
        {
            transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        }

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
