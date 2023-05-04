using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Normal.Realtime;

public class LeverBehaviour : MonoBehaviour
{
    public bool resetCondition = false;

    // Change to be true when input by grabPressed is present
    //private bool isLeftGrabPressed = false;
    //private bool isRightGrabPressed = false;

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
    public GameObject ElevatorObj;


    // Start is called before the first frame update
    void Start()
    {
        //elevator = GameObject.Find("elevator_v2");
        syncedLeverData = GetComponent<LeverData>();
        audioSource = GetComponent<AudioSource>();
        GameManagerReference = GameObject.Find("GameManager");
        ElevatorObj = GameObject.Find("elevator_v2");
    }

    // Update is called once per frame
    void Update()
    {
        CheckElevatorPosition(); // Sets resetCondition for lever to true.
        CheckForResetLever(); // Resets levers

        if (syncedLeverData._leversPulled == 1 && !colorSet)
        {
            mat = meshRenderer.material;
            mat.SetColor("_EmissionColor", Color.green);
            audioSource.PlayOneShot(pulled, 0.7f);
            colorSet = true;
        }
        else if (syncedLeverData._leversPulled == 0)
        {
            mat = meshRenderer.material;
            mat.SetColor("_EmissionColor", Color.red);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!wasPulled && other.tag == "Hands" && (other.GetComponentInParent<PlayerData>()._backupBool || other.GetComponentInParent<PlayerData>()._isReady))
        {
            //Debug.Log("OnTrigger - If Passed");
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
            if (other.GetComponentInParent<RealtimeTransform>().isOwnedLocallySelf) return;
            syncedLeverData._leversPulled = 1; // Now means that its pulled and should set color.
            GameManagerReference.GetComponent<GameManagerData>()._level++; // A variable to keep track of how many levers has been pulled.
            wasPulled = true;
        }
    }

    private void CheckForResetLever() // still needs adjustment
    {
        if (resetCondition)
        {
            if (GameManager.IsServer) // only server does this
            {
                gameObject.GetComponent<RealtimeTransform>().RequestOwnership();

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
                syncedLeverData._leversPulled = 0;
            }
            wasPulled = false; // needs to be reset for both client and server
            resetCondition = false; // needs to be reset for both client and server
        }
    }

    public void CheckElevatorPosition()
    {
        if (ElevatorObj.transform.position.y < -90)
        {
            resetCondition = true;
        }
    }
}
