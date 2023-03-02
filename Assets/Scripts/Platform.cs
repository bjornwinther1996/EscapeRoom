using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isSolid;
    private float timer;
    public float timerThreshold = 1;
    Collider collider;
    int randomChance; //Temporary - functionality should be in grid class

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();

        // Below is temporary - set from grid/path class in future.
        randomChance = Random.Range(0,4);
        if (randomChance == 2)
        {
            isSolid = true;
        }
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
        if (timer <= timerThreshold) { return; }//break instead?
        checkPlatform();


    }

    public void SetSolid(bool isSolid)
    {
        this.isSolid = isSolid;
    }

    public void GlassCracking()
    {
        //Sound?
        //animation or material change.
    }

    public void platformFall()
    {
        //make platform fall (translate pos), and possibly delete?
        gameObject.SetActive(false);
    }

    public void Success()
    {
        //Sound
        //light up perimiter of platform
        //material change?
    }

    public void checkPlatform()
    {
        if (isSolid)
        {
            Success();
        }
        else
        {
            GlassCracking();
            if (timer >= timerThreshold + 0.5)
            {
                platformFall();
            }
        }
    }

}
