using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnLever : MonoBehaviour
{

    private bool runOnce = false;
    private float timer;

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
            
            GameObject lever = Realtime.Instantiate("Lever", transform.position * 10, transform.rotation, new Realtime.InstantiateOptions
            {
                ownedByClient = false,
                preventOwnershipTakeover = false,
                destroyWhenOwnerLeaves = false,
                destroyWhenLastClientLeaves = true
            });

            runOnce = true;
        }
    }
}
