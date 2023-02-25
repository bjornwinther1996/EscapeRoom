using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isSolid;
    private float timer;
    public float timerThreshold = 1;
    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other) // can use courutine instead? - to wait x-time to execute.
    {
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
        }

        //the following is the same as the above:
        /*
        if (!other.CompareTag("player")) { return; }
        timer += Time.deltaTime;
        if (timer <= timerThreshold) { return; }
        */
    }

    public void SetSolid(bool isSolid)
    {
        isSolid = this.isSolid;
    }

    public void GlassCracking()
    {
        //Sound?
        //animation or material change.
    }

    public void platformFall()
    {
        //make platform fall (translate pos), and possibly delete?
    }

    public void Success()
    {
        //Sound
        //light up perimiter of platform
        //material change?
    }

}
