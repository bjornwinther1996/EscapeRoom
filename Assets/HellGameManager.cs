using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class HellGameManager : MonoBehaviour
{
    public GameObject LeverMount;
    private bool instantiated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsServer) return;

        if (instantiated) return;
        GameObject lever = Realtime.Instantiate("Lever_back", transform.position, transform.rotation, new Realtime.InstantiateOptions
        {
            ownedByClient = false,
            preventOwnershipTakeover = false,
            destroyWhenOwnerLeaves = false,
            destroyWhenLastClientLeaves = true
        });
        lever.GetComponent<RealtimeTransform>().RequestOwnership();
        lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
        lever.transform.position = LeverMount.transform.position;
        //HingeJoint hj = lever.GetComponent<HingeJoint>();
        //hj.connectedAnchor = new Vector3(LeverMount.transform.position.x + 0.02f, LeverMount.transform.position.y + 0.01f, LeverMount.transform.position.z - 0.02f);
        instantiated = true;
    }
}
