using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnLever : MonoBehaviour
{
    private bool runOnce = false;
    private float timer;

    private GameObject lever;
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
            if (gameObject.tag == "inverseMount")
            {
                lever = Realtime.Instantiate("Lever_inv", transform.position, transform.rotation, new Realtime.InstantiateOptions
                {
                    ownedByClient = false,
                    preventOwnershipTakeover = false,
                    destroyWhenOwnerLeaves = false,
                    destroyWhenLastClientLeaves = true
                });
                lever.transform.rotation = Quaternion.Euler(45, 0, 0);
            }
            else
            {
                lever = Realtime.Instantiate("Lever", transform.position, transform.rotation, new Realtime.InstantiateOptions
                {
                    ownedByClient = false,
                    preventOwnershipTakeover = false,
                    destroyWhenOwnerLeaves = false,
                    destroyWhenLastClientLeaves = true
                });
                lever.transform.rotation = Quaternion.Euler(-45, 0, 0);
            }

            hj = lever.GetComponent<HingeJoint>();
            if (gameObject.tag == "inverseMount")
            {
                hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z - 0.02f);
            }
            else
            {
                hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
            }
            Debug.Log("Rotation set for Lever");

            //lever.transform.Rotate(90f, 0, 0, Space.Self);

            /*if (gameObject.tag == "inverseMount") 
            {
                gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                lever.GetComponent<RealtimeTransform>().RequestOwnership();
                Debug.Log("WAS TRIGGERED DAWG");
                lever.transform.Rotate(0, -180f, 0);
            }
            */

            runOnce = true;
        }
    }
}
