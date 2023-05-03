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

    float previousYPos;
    //public int PlayerNumber; // Is now syncedPlayerData._BackupInt instead

    public Material Player1AvatarMat;
    public Material Player2AvatarMat;
    public GameObject AvatarMeshObj;
    bool isAvatarMaterialSet;

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
        //if (Application.platform != RuntimePlatform.Android) { return; } // if computer - return
        SetHandsColor();
        SetAvatarColor();
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        SetPlayerNumber(); // PlayerNumber is now set with sync variable and can therefore be set ownedlocally by client self.

        if (syncedPlayerData._isServer)
        {
            GameManager.IsServer = true;
        }
        else
        {
            GameManager.IsServer = false;
        }

        /*
        if (Application.platform == RuntimePlatform.Android)
        {
            PlayerNumber = 3;
        }*/

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
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        if (!ChangeInYPos()) { return; } // Return if no change in Y pos.
        //Debug.Log("Change in Y pos");

        if (transform.position.y > -89 && transform.position.y < -3)
        {
            if (imageColor.a < 1f)
            {
                fadeTimer += Time.deltaTime;
                imageColor.a += (fadeTimer / 0.5f) * Time.deltaTime;
                imageComponent.color = imageColor;
            }
        }
        
    }

    private void SetHandsColor() // needs color sync?
    {
        if (!GameManagerReference.GetComponent<GameManagerData>()._backupBool) { return; }
        if (isHandsColorSet) { return; }
        if (syncedPlayerData._isServer && LeftHand.GetComponent<SkinnedMeshRenderer>().material != Player1AvatarMat)
        {
            LeftHand.GetComponent<SkinnedMeshRenderer>().material = Player1AvatarMat;
            RightHand.GetComponent<SkinnedMeshRenderer>().material = Player1AvatarMat;
        }
        else if(!syncedPlayerData._isServer && LeftHand.GetComponent<SkinnedMeshRenderer>().material != Player2AvatarMat)
        {
            LeftHand.GetComponent<SkinnedMeshRenderer>().material = Player2AvatarMat;
            RightHand.GetComponent<SkinnedMeshRenderer>().material = Player2AvatarMat;
        }
        isHandsColorSet = true;
    }

    private void SetAvatarColor()
    {
        if (!GameManagerReference.GetComponent<GameManagerData>()._backupBool) { return; }
        if (isAvatarMaterialSet) { return; }
        
        if (syncedPlayerData._isServer && AvatarMeshObj.GetComponent<SkinnedMeshRenderer>().material != Player1AvatarMat)
        {
            AvatarMeshObj.GetComponent<SkinnedMeshRenderer>().material = Player1AvatarMat;
        }
        else if (!syncedPlayerData._isServer && AvatarMeshObj.GetComponent<SkinnedMeshRenderer>().material != Player2AvatarMat)
        {
            AvatarMeshObj.GetComponent<SkinnedMeshRenderer>().material = Player2AvatarMat;
        }
        isAvatarMaterialSet = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        /*
        if (other.gameObject.tag == "Elevator")
        {
            //Debug.Log("ELLIE HIT");
            if (!other.gameObject == null)
            {
                other.GetComponent<RealtimeTransform>().RequestOwnership();
            }
        }*/

        if (other.CompareTag("HellHitbox"))
        {
            imageColor.a = 0;
            imageComponent.color = imageColor;
            fadeTimer = 0;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if (other.gameObject.tag == "Elevator")
        {
            Vector3 newPos = new Vector3(VRRig.transform.position.x, other.transform.position.y - avatarYOffset, VRRig.transform.position.z);
            //Debug.Log("STILL IN ELLIE");

            VRRig.transform.position = newPos;
        }*/

    }

    public bool ChangeInYPos()
    {
        if (previousYPos !=transform.position.y)
        {
            previousYPos = transform.position.y;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerNumber()
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        if (Application.platform != RuntimePlatform.Android) // if coomputer:
        {
            //PlayerNumber = 3;
            syncedPlayerData._backupInt = 3;
        }
        else if (syncedPlayerData._isServer)
        {
            //PlayerNumber = 1;
            syncedPlayerData._backupInt = 1;
        }
        else
        { 
            //PlayerNumber = 2;
            syncedPlayerData._backupInt = 2;
        }
    }

}
