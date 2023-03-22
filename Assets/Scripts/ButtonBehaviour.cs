using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{

    public ElevatorData elevatorData; 
    
    public float waitTime = 2f;
    
    public Animator ElevatorAnims;

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;


    void Start()
    {
        
        sound = GetComponent<AudioSource>();
        isPressed = false;
        ElevatorAnims = GetComponentInParent<Animator>();
        elevatorData = GetComponentInParent<ElevatorData>();

    }

    void FixedUpdate()
    {
        //ElevatorAnims.SetBool("GoUp", elevatorData._goUp);
        //ElevatorAnims.SetBool("GoDown", true);

        if (elevatorData._goUp)
        {
            ElevatorAnims.SetBool("GoUp", true);
            WaitAndReset();
            ElevatorAnims.SetBool("GoDown", false);
        }
        else if (elevatorData._goDown)
        {
            ElevatorAnims.SetBool("GoDown", true);
            WaitAndReset();
            ElevatorAnims.SetBool("GoUp", false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        button.transform.localPosition = new Vector3(0, 0.01f, 0);
        onRelease.Invoke();
        isPressed = false;
        
    }

    public void ElevatorGoUp()
    {
        elevatorData._goUp = true;
        //ElevatorAnims.SetBool("GoUp", elevatorData._goUp);
        
        if (true)
        {
            DebuggerVR.DebugMessage1 = "Starting coroutine UP";
            StartCoroutine(WaitAndReset());
        }

        elevatorData._goDown = false;
        //ElevatorAnims.SetBool("GoDown", elevatorData._goDown);
    }

    public void ElevatorGoDown()
    {
        elevatorData._goDown = true;
        //ElevatorAnims.SetBool("GoDown", elevatorData._goDown);
        if (true)
        {
            DebuggerVR.DebugMessage1 = "Starting coroutine DOWN";
            StartCoroutine(WaitAndReset());
        }
        //ElevatorAnims.SetBool("GoUp", false);
        elevatorData._goUp = false;
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(2.0f);
    }

}
