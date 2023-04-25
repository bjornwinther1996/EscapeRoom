using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{

    private bool wasPulled = false;
    private LeverData syncedLeverData;

    // Start is called before the first frame update
    void Start()
    {
        syncedLeverData = GetComponent<LeverData>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!wasPulled && this.transform.rotation.x > 0.9f)
        {
            syncedLeverData._leversPulled += 1;
            Debug.Log("Amount of levers pulled = " + syncedLeverData._leversPulled);
            wasPulled = true;
            // Play Audio
        }
    }
}
