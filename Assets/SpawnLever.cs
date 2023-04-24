using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnLever : MonoBehaviour
{
    private bool runOnce = false;
    private float timer;
    private HingeJoint hj;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!GameManager.IsServer) { return; }
        if (!runOnce && timer > 3)
        {
            
            GameObject lever = Realtime.Instantiate("Lever", transform.position, transform.rotation, new Realtime.InstantiateOptions
            {
                ownedByClient = false,
                preventOwnershipTakeover = false,
                destroyWhenOwnerLeaves = false,
                destroyWhenLastClientLeaves = true
            });

            hj = lever.GetComponent<HingeJoint>();
            hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
            lever.transform.Rotate(120f, 0, 0, Space.Self);            

            runOnce = true;
        }
    }
}
