using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour
{

    public Transform PlayerHead;
    public CapsuleCollider BodyCollider;

    private float bodyHeightMin = 0.5f;
    private float bodyHeightMax = 2.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        BodyCollider.height = Mathf.Clamp(PlayerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        BodyCollider.center = new Vector3(PlayerHead.localPosition.x, BodyCollider.height / 2, PlayerHead.localPosition.z);
    }
}
