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
    //int randomChance; //Temporary - functionality should be in grid class

    //platform needs to have realtime components on them! - and this script needs to get the realtime component to delete realtime etc.

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        audioSource = GetComponentInParent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) // can use courutine instead? - to wait x-time to execute. // Rigidbody on Avatar
    {
        /*
        if (other.CompareTag("player")) //change this to "returnIF" but not the other code below)
        {
            timer += Time.deltaTime; // test if this works xD
            if (timer >= timerThreshold)
            {
                if (isSolid)
                {
                    Success();
                }
                else
                {
                    GlassCracking();
                    if (timer >= timerThreshold+0.5)
                    {
                        platformFall();
                    }
                }
                
            }
        }*/

        //the following is the same as the above: - check if it works
        if (!other.CompareTag("Player")) { return; }
        timer += Time.deltaTime;
        if (timer <= TimerThreshold) { return; }//break instead?
        checkPlatform();


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
        gameObject.SetActive(false); // probably has to be changed to delete and furthermore realtime.delete
    }

    public void Success()
    {
        //Sound
        //light up perimiter of platform
        //material change?
        if(stopCalling) { return; }
        platformActivated = true;
        stopCalling = true;
    }

    public void checkPlatform()
    {
        if (IsSolid)
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

    public bool GetPlatformActivated()
    {
        return platformActivated;
    }

    public void SetPlatformActivated(bool platformActivated)
    {
        this.platformActivated = platformActivated;
    }

}
