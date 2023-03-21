using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : MonoBehaviour
{

    public Animator gate; 

    public bool access = false;

    // Start is called before the first frame update
    void Start()
    {
        gate = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (access)
        {
            gate.SetBool("accessToHeaven", access);
        }
        else if (!access)
        {
            gate.SetBool("accessToHeaven", access);
            return;
        }
    }
}
