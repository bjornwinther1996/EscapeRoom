using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Normal.Realtime;

public class AnimateHandOnInput : MonoBehaviour
{

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    private Animator handAnimator;
    private HandAnimationSyncTest handAnimationSyncTest;

    // Start is called before the first frame update
    void Start()
    {
        GetDevice();
        handAnimator = GetComponent<Animator>();
        handAnimationSyncTest = GetComponent<HandAnimationSyncTest>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (GetComponentInParent<RealtimeTransform>().isOwnedLocallySelf)
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                //handAnimator.SetFloat("Trigger", triggerValue);
                //handAnimationSyncTest._triggerValue = triggerValue;

                handAnimator.SetFloat("Trigger", triggerValue);
            } 
            else {
                handAnimator.SetFloat("Trigger", 0);
            }

            if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                handAnimator.SetFloat("Grip", gripValue);
            } 
            else {
                handAnimator.SetFloat("Grip", 0);
            }

        }
        */

        UpdateHandAnimation();

    }

    void GetDevice()
    {

        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    void UpdateHandAnimation()
    {
        if (GetComponentInParent<RealtimeTransform>().isOwnedLocallySelf)
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                //handAnimator.SetFloat("Trigger", triggerValue);
                handAnimationSyncTest._triggerValue = triggerValue;



            }
            else
            {
                //handAnimator.SetFloat("Trigger", 0);
                handAnimationSyncTest._triggerValue = 0;
            }

            if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                //handAnimator.SetFloat("Grip", gripValue);
                handAnimationSyncTest._gripValue = gripValue;
            }
            else
            {
                //handAnimator.SetFloat("Grip", 0);
                handAnimationSyncTest._gripValue = 0;
            }
        }
    }

}
