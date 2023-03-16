using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{

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
        ElevatorAnims.SetBool("GoUp", true);
        if (true)
        {
            DebuggerVR.DebugMessage1 = "Starting coroutine UP";
            StartCoroutine(WaitAndReset());
        }
        ElevatorAnims.SetBool("GoDown", false);
    }

    public void ElevatorGoDown()
    {
        ElevatorAnims.SetBool("GoDown", true);
        if (true)
        {
            DebuggerVR.DebugMessage1 = "Starting coroutine DOWN";
            StartCoroutine(WaitAndReset());
        }
        ElevatorAnims.SetBool("GoUp", false);
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(2.0f);
    }

}
