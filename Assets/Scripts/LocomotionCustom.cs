using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionCustom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject head;
    bool isVerticalLocked;
    bool isHorizontalLocked;
    Vector3 headRotationVerticalVector;
    Vector3 headRotationHorizontalVector;

    float checkLocomotionChangeStartTime;
    bool isLocomotionEnabled;

    bool runOnce;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocomotionEnabled)
        {
            LocomotionControl();
        }
        
    }

    private void Update()
    {
        if (CheckForLocomotionChange())
        {
            if (Time.time > checkLocomotionChangeStartTime + 2)
            {
                if (!runOnce)
                {
                    isLocomotionEnabled = !isLocomotionEnabled;
                    runOnce = true;
                }
                
            }
        }
        else
        {
            checkLocomotionChangeStartTime = Time.time;
            runOnce = false;
        }

        //DebuggerVR.debuggingString = CheckForLocomotionChange().ToString();
    }

    bool CheckForLocomotionChange()
    {
        bool isLeftPressed = false;
        bool isRightPressed = false;

        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = leftHandDevices[0];

            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out triggerValue) && triggerValue)
            {
                isLeftPressed = true;
            }
            else
            {
                isLeftPressed = false;
            }
        }

        if (rightHandDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = rightHandDevices[0];

            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out triggerValue) && triggerValue)
            {
                isRightPressed = true;
            }
            else
            {
                isRightPressed = false;
            }
        }

        if (isLeftPressed && isRightPressed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void LocomotionControl()
    {

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        Vector2 triggerValue = new Vector2();
        float scalar = 1f;

        if (!isVerticalLocked)
        {
            headRotationVerticalVector = new Vector3(head.transform.forward.x, 0, head.transform.forward.z);
        }

        if (!isHorizontalLocked)
        {
            headRotationHorizontalVector = new Vector3(head.transform.right.x, 0, head.transform.right.z);
        }

        foreach (var device in inputDevices)
        {
            //if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out triggerValue))
            //{ 

            //}

            

            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out triggerValue))
            {
                //HORIZONTAL
                if (triggerValue.x > 0.1)
                {
                    isHorizontalLocked = true;
                    transform.position += headRotationHorizontalVector * triggerValue.x * scalar * Time.deltaTime;

                }
                else if (triggerValue.x < -0.1)
                {
                    isHorizontalLocked = true;
                    transform.position += headRotationHorizontalVector * triggerValue.x * scalar * Time.deltaTime;
                }
                else
                {
                    isHorizontalLocked = false;
                }


                //VERTICAL
                if (triggerValue.y > 0.1)
                {
                    isVerticalLocked = true;
                    transform.position += headRotationVerticalVector * triggerValue.y * scalar * Time.deltaTime;

                } else if (triggerValue.y < -0.1)
                {
                    isVerticalLocked = true;
                    transform.position += headRotationVerticalVector * triggerValue.y * scalar * Time.deltaTime;
                } else
                {
                    isVerticalLocked = false;
                }
            

            }
        }

        //DebuggerVR.debuggingString = triggerValue.ToString();
    }
}
