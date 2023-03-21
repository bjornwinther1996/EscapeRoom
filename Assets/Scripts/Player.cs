using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerData syncedPlayerData;
    public GameObject VRRig;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject GameManagerReference;
    public Material MaterialPlayer1;
    public Material MaterialPlayer2;
    private bool isHandsColorSet;

    private float avatarYOffset = 2f;

    void Start()
    {
        syncedPlayerData = GetComponent<PlayerData>();
        VRRig = GameObject.Find("VR Player");
        GameManagerReference = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;

        if (syncedPlayerData._isServer)
        {
            GameManager.IsServer = true;
        }
        else
        {
            GameManager.IsServer = false;
        }

        SetHandsColor();
    }

    private void SetHandsColor() // needs color sync?
    {
        if (!GameManagerReference.GetComponent<GameManagerData>()._backupBool) { return; }
        if (isHandsColorSet) { return; }
        if (GameManager.IsServer)
        {
            LeftHand.GetComponent<MeshRenderer>().material = MaterialPlayer1;
            RightHand.GetComponent<MeshRenderer>().material = MaterialPlayer1;
        }
        else if(!GameManager.IsServer)
        {
            LeftHand.GetComponent<MeshRenderer>().material = MaterialPlayer2;
            RightHand.GetComponent<MeshRenderer>().material = MaterialPlayer2;
        }
        isHandsColorSet = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Elevator")
        {
            //Debug.Log("ELLIE HIT");
            if (!other.gameObject == null)
            {
                other.GetComponent<RealtimeTransform>().RequestOwnership();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Elevator")
        {
            Vector3 newPos = new Vector3(VRRig.transform.position.x, other.transform.position.y - avatarYOffset, VRRig.transform.position.z);
            //Debug.Log("STILL IN ELLIE");

            VRRig.transform.position = newPos;
        }


    }

}
