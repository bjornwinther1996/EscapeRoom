using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool IsSolid;
    private float timer;
    public float TimerThreshold = 1;
    Collider collider;
    public AudioClip[] VanishSounds;
    private AudioSource audioSource;
    private bool platformActivated;
    private bool stopCalling;
    private MeshRenderer meshRenderer;
    public Material Player1Material;
    public Material Player2Material;
    public GameObject GameManagerReference;

    private PlatformData syncedPlatformVariables;
    private bool isMaterialSet;
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
        Debug.Log("GameManagerReference: " + GameManagerReference);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerReference.GetComponent<GameManagerData>()._backupBool) { return; }
        SetMaterial();
    }

    public void SetMaterial()
    {
        if (isMaterialSet) { return; }
        if (GameManager.Player1 && syncedPlatformVariables._isSolidPlayer1)
        {
            meshRenderer.material = Player1Material;
        }
        else if(GameManager.Player2 && syncedPlatformVariables._isSolidPlayer2)
        {
            meshRenderer.material = Player2Material;
        }
        isMaterialSet = true;
    }

    private void OnTriggerStay(Collider other) // can use courutine instead? - to wait x-time to execute. // Rigidbody on Avatar
    {
        if (!other.CompareTag("Player")) { return; }
        timer += Time.deltaTime;
        if (timer <= TimerThreshold) { return; }//break instead?
        //CheckPlatformBothPlayers();
        CheckPlatformOnePlayer(GameManager.Player1, syncedPlatformVariables._isSolidPlayer1);
        CheckPlatformOnePlayer(GameManager.Player2, syncedPlatformVariables._isSolidPlayer2);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Throwable")) { return; }
        timer += Time.deltaTime;
        CheckPlatformThrowable(0.1f);
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
        gameObject.SetActive(false); // probably has to be changed to delete and furthermore realtime.delete?
    }

    public void Success()
    {
        //Sound
        //light up perimiter of platform
        //material change?
        if(stopCalling) { return; } // so it doesnt activate all the next rows, as you continue to stand on activated/correct platform. Only triggers once, revealing next row
        platformActivated = true;
        stopCalling = true;
    }

    public void CheckPlatformBothPlayers()
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
            }
        }
    }

    public void CheckPlatformOnePlayer(GameObject player, bool isSolidPlayer)
    {
        if (!player) { return;  }
        if (isSolidPlayer)
        {
            Success();
        }
        else
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

}
