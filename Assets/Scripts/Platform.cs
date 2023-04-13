using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Platform : MonoBehaviour
{
    public bool IsSolid;
    private float timer;
    public float TimerThreshold = 1;
    Collider collider;
    public AudioClip[] VanishSounds;
    public AudioClip SuccessAudio;
    private AudioSource audioSource;
    private bool platformActivated;
    private bool stopCalling;
    private MeshRenderer meshRenderer;
    private Material defaultMaterial;
    public Material Player1Material;
    public Material Player2Material;
    public GameObject GameManagerReference;
    private float materialTimer;
    private bool platformDisabled;

    private PlatformData syncedPlatformVariables;
    private bool isMaterialSet;

    public static int NumberOfPlatformsDestroyed;
    public static int NumberOfMaterialsChanged;

    public Vector3 SpawnPosition; // Is set in PlatformManager when Instantiated
    public Vector3 DespawnPosition; // Is set in PlatformManager when Instantiated
    //int randomChance; //Temporary - functionality should be in grid class

    //platform needs to have realtime components on them! - and this script needs to get the realtime component to delete realtime etc.

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        audioSource = GetComponentInParent<AudioSource>();
        syncedPlatformVariables = GetComponent<PlatformData>();
        meshRenderer = GetComponent<MeshRenderer>();
        //GameManagerReference = GameObject.FindGameObjectWithTag("GameManager");
        GameManagerReference = GameObject.Find("GameManager");
        //Debug.Log("GameManagerReference: " + GameManagerReference);
        defaultMaterial = meshRenderer.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerReference.GetComponent<GameManagerData>()._backupBool) { return; } // if platforms are instantiated (Realtime) Into the scene
        SetMaterial(); // CHANGE SO IT IS CALLED WHEN YOU STAND ON THE INITIAL READY/START BUTTON AS A PLAYER
        
        if (GameManagerReference.GetComponent<GameManagerData>()._backupFloat > 0 && !GameManager.IsServer) // this is only done for client, as it is done for server in GameManger
        {
            StartCoroutine(ResetMaterialTimer(8)); // if this timer is adjusted - remember to adjust for server accordingly
            NumberOfMaterialsChanged++;
            if (NumberOfMaterialsChanged >= PlatformManager.COLOUMNLENGTH * PlatformManager.RowLength)
            {
                NumberOfMaterialsChanged = 0;
                GameManagerReference.GetComponent<GameManagerData>()._backupFloat = 0;
            } 
        }
        
    }

    public void SetMaterial() // only called locally on each client // In future, SetMaterial, will be called once you stand on something!!!!!!!!
    {
        if (isMaterialSet) { return; }
        materialTimer += Time.deltaTime; // store time
        if (materialTimer < 3) { return; } // check if time is passed to counter bug where material is set before platforms are RealtimeInstantiated
            if (GameManager.IsServer && syncedPlatformVariables._isSolidPlayer2)
        {
            meshRenderer.material = Player2Material;
        }
        else if(!GameManager.IsServer && syncedPlatformVariables._isSolidPlayer1)
        {
            meshRenderer.material = Player1Material;
        }
        else
        {
            meshRenderer.material = defaultMaterial;
        }
        isMaterialSet = true;
    }

    public void ResetMaterial() // only called locally on each client // In future, SetMaterial, will be called once you stand on something!!!!!!!!
    {
        if (GameManager.IsServer && syncedPlatformVariables._isSolidPlayer2)
        {
            meshRenderer.material = Player2Material;
        }
        else if (!GameManager.IsServer && syncedPlatformVariables._isSolidPlayer1)
        {
            meshRenderer.material = Player1Material;
        }
        else
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    public IEnumerator ResetMaterialTimer(float time) // only called locally on each client // In future, SetMaterial, will be called once you stand on something!!!!!!!!
    {
        yield return new WaitForSeconds(time);
        if (GameManager.IsServer && syncedPlatformVariables._isSolidPlayer2)
        {
            meshRenderer.material = Player2Material;
        }
        else if (!GameManager.IsServer && syncedPlatformVariables._isSolidPlayer1)
        {
            meshRenderer.material = Player1Material;
        }
        else
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    private void OnTriggerStay(Collider other) // can use courutine instead? - to wait x-time to execute. // Rigidbody on Avatar
    {
        if (!other.CompareTag("Player")) { return; }
        timer += Time.deltaTime;
        if (timer <= TimerThreshold) { return; }//break instead?
        CheckPlatformOld();
        //CheckPlatformForPlayers(); // doesnt work yet

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Throwable")) { return; }
        timer += Time.deltaTime;
        CheckPlatformThrowable(0f);
    }

    public void SetSolid(bool isSolid)
    {
        this.IsSolid = isSolid;
    }

    public void GlassCracking()
    {
        //Sound?
        //animation or material change.
    }

    public void PlatformFall()
    {
        //make platform fall (translate pos), and possibly delete?
        int randomAudio = Random.Range(0, 3);
        switch (randomAudio)
        {
            case 0:
                audioSource.PlayOneShot(VanishSounds[0]);
                break;
            case 1:
                audioSource.PlayOneShot(VanishSounds[1]);
                break;
            case 2:
                audioSource.PlayOneShot(VanishSounds[2]);
                break;
            default:
                audioSource.PlayOneShot(VanishSounds[1]);
                break;
        }
        platformDisabled = true;
        DespawnPlatform();
        timer = 0;
    }

    public void Success()
    {
        Debug.Log("Success Method called in Platform");
        //Sound
        //light up perimiter of platform
        //material change?
        audioSource.PlayOneShot(SuccessAudio);
        if (stopCalling) { return; } // so it doesnt activate all the next rows, as you continue to stand on activated/correct platform. Only triggers once, revealing next row
        Debug.Log("Guard Clause in Success Platform passed");
        platformActivated = true;
        stopCalling = true;
        timer = 0;
    }

    public void CheckPlatformOld() // old - not used anymore.
    {
        if (syncedPlatformVariables._isSolidPlayer1 || syncedPlatformVariables._isSolidPlayer2)
        {
            Success();
        }
        else
        {
            GlassCracking();
            if (timer >= TimerThreshold + 1)
            {
                PlatformFall();
                NumberOfPlatformsDestroyed++;
            }
        }
    }

    public void CheckPlatformForPlayers()
    {

        if (GameManager.IsServer && syncedPlatformVariables._isSolidPlayer1)
        {
            Success();
        }
        else if (!GameManager.IsServer && syncedPlatformVariables._isSolidPlayer2)
        {
            Success();
        }
        else if(!syncedPlatformVariables._isSolidPlayer1 || !syncedPlatformVariables._isSolidPlayer2)
        {
            GlassCracking();
            if (timer >= TimerThreshold + 1)
            {
                PlatformFall();
            }
        }
    }

    public void CheckPlatformThrowable(float timeThreshold)
    {
        if (syncedPlatformVariables._isSolidPlayer1 || syncedPlatformVariables._isSolidPlayer2)
        {
            Success();
        }
        else
        {
            GlassCracking();
            if (timer >= timeThreshold)
            {
                PlatformFall();
            }
        }
    }

    public bool GetPlatformActivated()
    {
        return platformActivated;
    }

    public void SetPlatformActivated(bool platformActivated)
    {
        this.platformActivated = platformActivated;
    }

    public void DespawnPlatform() // DOES THIS FUCK THE SOUND??
    {
        if (!GameManager.IsServer) { return; }
        //gameObject.SetActive(false); // probably has to be changed to delete and furthermore realtime.delete?
        gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
        gameObject.transform.position = DespawnPosition;
    }

    public bool GetPlatformDisabled()
    {
        return platformDisabled;
    }

    public void SetPlatformDisabled(bool platformDisabled)
    {
        this.platformDisabled = platformDisabled;
    }

    public void SpawnPlatform()
    {
        if (!GameManager.IsServer) { return; }
        //gameObject.SetActive(true); // probably has to be changed to delete and furthermore realtime.delete?
        gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
        gameObject.transform.position = SpawnPosition;
    }

    public bool GetStopCalling()
    {
        return stopCalling;
    }

    public void SetStopCalling(bool stopCalling)
    {
        this.stopCalling = stopCalling;
    }

}
