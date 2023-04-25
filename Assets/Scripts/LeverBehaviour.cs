using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{

    private static int leversPulledGlobal;
    
    private bool wasPulled = false;
    private LeverData syncedLeverData;
    private ElevatorData syncedElevatorData;

    // Start is called before the first frame update
    void Start()
    {
        syncedLeverData = GetComponent<LeverData>();
        leversPulledGlobal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.rotation.x);

        if (!wasPulled && this.gameObject.name == "Lever_back(Clone)" && this.transform.rotation.x > 0.9f)
        {
            leversPulledGlobal += 1;
            syncedLeverData._leversPulled = leversPulledGlobal;
            Debug.Log("Amount of levers pulled = " + syncedLeverData._leversPulled);
            wasPulled = true;
            // Play Audio
        }
        else if (!wasPulled && this.gameObject.name == "Lever_front(Clone)" && this.transform.rotation.x > 0.9f)
        {
            leversPulledGlobal += 1;
            syncedLeverData._leversPulled = leversPulledGlobal;
            Debug.Log("Amount of levers pulled = " + syncedLeverData._leversPulled);
            wasPulled = true;
            // Play Audio
        }
    
    }
}
