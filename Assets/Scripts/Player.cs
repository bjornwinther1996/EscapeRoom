using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;

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

    public Material HeavenSkyMaterial;
    public Material HellSkyMaterial;
    
    public GameObject UICanvas;

    public GameObject ImageFadeCanvas;
    private Image imageComponent;
    private Color imageColor;

    float fadeTimer;

    void Start()
    {
        syncedPlayerData = GetComponent<PlayerData>();
        VRRig = GameObject.Find("VR Player");
        GameManagerReference = GameObject.Find("GameManager");

        imageComponent = ImageFadeCanvas.GetComponent<Image>();
        imageColor = Color.black;
        imageColor.a = 0f;
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
        SetSkybox();
        ActivateFadeWhenFalling();
    }

    private void SetSkybox()
    {
        if (transform.position.y > -50 && RenderSettings.skybox != HeavenSkyMaterial) // LOGIC SO IT ONLY CALLS ONCE.
        {
            RenderSettings.skybox = HeavenSkyMaterial;
            
        }
        else if(transform.position.y < -50 && RenderSettings.skybox != HellSkyMaterial)
        {
            RenderSettings.skybox = HellSkyMaterial;
            
        }
        //DynamicGI.UpdateEnvironment();
    }

    public void ActivateFadeWhenFalling()
    { // if change in Y position condition?
        if (transform.position.y > -85 && transform.position.y < -10)
        {
            if (imageColor.a < 1f)
            {
                fadeTimer += Time.deltaTime;
                imageColor.a += (fadeTimer / 0.5f) * Time.deltaTime;
                imageComponent.color = imageColor;
            }
        }
        else if (transform.position.y < -85)
        {
            imageColor.a = 0;
            imageComponent.color = imageColor;
            fadeTimer = 0;
        }
        
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
