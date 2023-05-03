using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnLever : MonoBehaviour
{
    private bool runOnce = false;

    public GameObject lever;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.IsServer)
        {
            if (!runOnce && GameManager.GameStarted)
            {
                if (gameObject.tag == "inverseMount")
                {
                    lever = Realtime.Instantiate("Lever_back", new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z), Quaternion.Euler(transform.rotation.x + 45, transform.rotation.y, transform.position.z), new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.GetComponent<RealtimeTransform>().RequestOwnership();
                    //lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
                }
                else if (gameObject.tag == "leftMount")
                {
                    lever = Realtime.Instantiate("Lever_left", transform.position, Quaternion.Euler(transform.rotation.x + 45, transform.rotation.y + 90, transform.rotation.z), new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.GetComponent<RealtimeTransform>().RequestOwnership();
                    //lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
                }
                else if (gameObject.tag == "rightMount")
                {
                    lever = Realtime.Instantiate("Lever_right", transform.position, Quaternion.Euler(transform.rotation.x + 45, transform.rotation.y - 90, transform.rotation.z), new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.GetComponent<RealtimeTransform>().RequestOwnership();
                    //lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
                }
                else
                {
                    lever = Realtime.Instantiate("Lever_front", transform.position, Quaternion.Euler(transform.rotation.x + 45, transform.rotation.y + 180, transform.rotation.z), new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.GetComponent<RealtimeTransform>().RequestOwnership();
                    //lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
                }
                runOnce = true;
            }
            
        }
    }
}
