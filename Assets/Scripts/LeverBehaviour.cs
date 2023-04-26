using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{

    private static int leversPulledGlobal;
    
    private bool wasPulled = false;
    private LeverData syncedLeverData;
    private ElevatorData syncedElevatorData;

    [SerializeField]
    private Material mat;

    [SerializeField]
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        syncedLeverData = GetComponent<LeverData>();
        leversPulledGlobal = 0;

        
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (!wasPulled && this.gameObject.name == "Lever_back(Clone)" && this.transform.rotation.eulerAngles.x > 110)
        {
            leversPulledGlobal += 1;
            syncedLeverData._leversPulled = leversPulledGlobal;
            Debug.Log("Amount of levers pulled = " + syncedLeverData._leversPulled);
            wasPulled = true;
            // Play Audio
        }
        else if (!wasPulled && this.gameObject.name == "Lever_front(Clone)" && this.transform.rotation.y < 0.45f)
        {
            leversPulledGlobal += 1;
            syncedLeverData._leversPulled = leversPulledGlobal;
            Debug.Log("Amount of levers pulled = " + syncedLeverData._leversPulled);
            wasPulled = true;
            // Play Audio
        }
        else if (!wasPulled && this.gameObject.name == "Lever_left(Clone)" && this.transform.localRotation.y < 0.45f)
        {
            leversPulledGlobal += 1;
            syncedLeverData._leversPulled = leversPulledGlobal;
            Debug.Log("Amount of levers pulled = " + syncedLeverData._leversPulled);
            wasPulled = true;
            // Play Audio
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!wasPulled && other.name == "ActivateTrigger")
        {
            leversPulledGlobal += 1;
            syncedLeverData._leversPulled = leversPulledGlobal;
            Debug.Log("Amount of levers pulled = " + syncedLeverData._leversPulled);
            wasPulled = true;
            mat = meshRenderer.material;
            mat.SetColor("_EmissionColor", Color.green);
        }
    }
}
