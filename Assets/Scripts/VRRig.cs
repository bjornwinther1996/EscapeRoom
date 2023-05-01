using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPosOffset;
    public Vector3 trackingRotOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPosOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap rightHand;
    public VRMap leftHand;

    private float turnSmoothness = 3.5f;

    public Transform headConstraint;
    public Vector3 headBodyOffset;
    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        //transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
        /*
        if(transform.eulerAngles.y > -20 && transform.eulerAngles.y < 20)
        {
            transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        }*/
        Vector3 fixedHeadConstraint = headConstraint.up;
        if (fixedHeadConstraint.y < 0.95)
        {
            transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
            //transform.forward = Vector3.ProjectOnPlane(fixedHeadConstraint, Vector3.up).normalized;
            //Debug.Log("IF FixedHeadConstraint:" + fixedHeadConstraint);
        }
        //transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        //Debug.Log("HeadUP:" + headConstraint.up);

        //Debug.Log("Y ROTATION:" + transform.rotation.y + "Y euler" + transform.eulerAngles.y);
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
