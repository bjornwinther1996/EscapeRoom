using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{
    public bool resetCondition = false;

    // Change to be true when input by grabPressed is present
    private bool isLeftGrabPressed = false;
    private bool isRightGrabPressed = false;

    private static int leversPulledGlobal;
    private LeverData syncedLeverData;
    
    private bool wasPulled = false;
    public bool IsReferenced;

    private GameObject elevator;

    private AudioSource audioSource;
    public AudioClip pulled;

    [SerializeField]
    private Material mat;

    [SerializeField]
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        elevator = GameObject.Find("elevator_v2");
        syncedLeverData = elevator.GetComponent<LeverData>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForVRInput();
        CheckForResetLever();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!wasPulled && other.tag == "Hands" && (isLeftGrabPressed || isRightGrabPressed))
        {
            if (this.gameObject.name == "Lever_front(Clone)")
            {
                this.transform.rotation = Quaternion.Euler(135, 180, 0);
            }
            else if (this.gameObject.name == "Lever_back(Clone)")
            {
                this.transform.rotation = Quaternion.Euler(135, 0, 0);
            }
            else if (this.gameObject.name == "Lever_left(Clone)")
            {
                this.transform.rotation = Quaternion.Euler(135, 90, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(135, 270, 0); 
            }

            syncedLeverData._leversPulled++;
            mat = meshRenderer.material;
            mat.SetColor("_EmissionColor", Color.green);
            audioSource.PlayOneShot(pulled, 0.7f);
            wasPulled = true;
        }
    }

    private void CheckForVRInput()
    {
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = leftHandDevices[0];

            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue) && triggerValue)
            {
                isLeftGrabPressed = true;
            }
            else
            {
                isLeftGrabPressed = false;
            }
        }

        if (rightHandDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = rightHandDevices[0];

            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue) && triggerValue)
            {
                isRightGrabPressed = true;
            }
            else
            {
                isRightGrabPressed = false;
            }
        }
    }

    private void CheckForResetLever()
    {

        if (resetCondition)
        {

            if (this.gameObject.name == "Lever_front(Clone)")
            {
                this.transform.rotation = Quaternion.Euler(45, 0, 0);
            }
            else if (this.gameObject.name == "Lever_back(Clone)")
            {
                this.transform.rotation = Quaternion.Euler(45, 0, 0);
            }
            else if (this.gameObject.name == "Lever_left(Clone)")
            {
                this.transform.rotation = Quaternion.Euler(45, 0, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(45, 0, 0);
            }

            syncedLeverData._leversPulled--;
            mat = meshRenderer.material;
            mat.SetColor("_EmissionColor", Color.red);
            wasPulled = false;
            resetCondition = false;
        }
    }
}
