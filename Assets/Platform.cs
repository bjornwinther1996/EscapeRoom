using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool solid;
    public float timer;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {
            timer += Time.deltaTime; // test if this works xD
            if (timer >= timerThreshold)
            {

            }
        }

        //the following is the same as the above:
        if (!other.CompareTag("player")) { return; }
        timer += Time.deltaTime;
    }
}
