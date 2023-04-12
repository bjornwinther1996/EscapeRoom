using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Normal.Realtime;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    public GameObject handModelChild;

    HandAnimationSyncTest handAnimationSyncTest;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
        handAnimationSyncTest = GetComponent<HandAnimationSyncTest>();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {

                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
            }

            //REPLACING INSTANTIATE OF PREFAB WITH EXISTING GAMEOBJECT
            spawnedHandModel = handModelChild;
            //spawnedHandModel = Instantiate(handModelPrefab, transform);
            //spawnedHandModel = Realtime.Instantiate("leftHand");

            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                //handAnimator.SetFloat("Trigger", triggerValue);
                //handAnimationSyncTest._triggerValue = triggerValue;
                //GetComponent<TriggerAnimation>().
                other.gameObject.GetComponent<TriggerAnimationSyncTest>()._triggerValue = triggerValue;


            }
            else
            {
                //handAnimator.SetFloat("Trigger", 0);
                //handAnimationSyncTest._triggerValue = 0;
                other.gameObject.GetComponent<TriggerAnimationSyncTest>()._triggerValue = 0;
            }
        }
    }
    */

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

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                if(spawnedHandModel)
                    spawnedHandModel.SetActive(false);
                if(spawnedController)
                    spawnedController.SetActive(true);
            }
            else
            {
                if (spawnedHandModel)
                    spawnedHandModel.SetActive(true);
                if (spawnedController)
                    spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
