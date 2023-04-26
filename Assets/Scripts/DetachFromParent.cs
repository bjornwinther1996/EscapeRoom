using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
