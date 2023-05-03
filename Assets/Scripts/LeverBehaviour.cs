using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Normal.Realtime;

public class LeverBehaviour : MonoBehaviour
{
    public bool resetCondition = false;

    // Change to be true when input by grabPressed is present
    private bool isLeftGrabPressed = false;
    private bool isRightGrabPressed = false;

    //private static int leversPulledGlobal;
    private LeverData syncedLeverData;
    
    private bool wasPulled = false;
    public bool IsReferenced;

    //private GameObject elevator;

    private AudioSource audioSource;
    public AudioClip pulled;

    [SerializeField]
    private Material mat;

    [SerializeField]
    private MeshRenderer meshRenderer;
    public GameObject GameManagerReference;

    bool colorSet;



    // Start is called before the first frame update
    void Start()
    {
        //elevator = GameObject.Find("elevator_v2");
        syncedLeverData = GetComponent<LeverData>();
        audioSource = GetComponent<AudioSource>();
        GameManagerReference = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForVRInput();
        CheckForResetLever();

        if (syncedLeverData._leversPulled == 1 && !colorSet)
        {
            mat = meshRenderer.material;
            mat.SetColor("_EmissionColor", Color.green);
            audioSource.PlayOneShot(pulled, 0.7f);
            colorSet = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        if (!wasPulled && other.tag == "Hands" && (isLeftGrabPressed || isRightGrabPressed))
        {
            gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
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

            syncedLeverData._leversPulled = 1; // Now means that its pulled and should set color.
            GameManagerReference.GetComponent<GameManagerData>()._level++; // A variable to keep track of how many levers has been pulled.
            wasPulled = true;
        }
    }

    private void CheckForVRInput()
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
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

    private void CheckForResetLever() // still needs adjustment
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
