using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebuggerVR : MonoBehaviour
{

    TextMeshPro textMesh;
    public GameObject TextMeshObj;
    
    public bool ToggleButton;

    public static string DebugMessage1;
    public static string DebugMessage2;
    public static string DebugMessage3;

    // Make sure to TOGGLE! Not only visible when holding down A but toggle on/off. 
    bool toggleReady = true;


    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        DebugMessage1 = "N/A";
        DebugMessage2 = "N/A";
        DebugMessage3 = "N/A";

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.SetText(DebugMessage1 + "\n" + DebugMessage2 + "\n" + DebugMessage3);

        ListenForInput();
        if (ToggleButton)
        {
            TextMeshObj.SetActive(true);
        }
        else
        {
            TextMeshObj.SetActive(false);
        }
    }

    void ListenForInput()
    {
        
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = rightHandDevices[0];

            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out triggerValue) && triggerValue)
            {
                if (toggleReady)
                {
                    ToggleButton = !ToggleButton;
                    StartCoroutine(Cooldown());
                }
             
            }

        }
    }

    private IEnumerator Cooldown()
    {
        toggleReady = false;
        yield return new WaitForSeconds(2.0f);
        toggleReady = true;
    }
}
 
