using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{

    public GameManager gameManager;
    public ElevatorData elevatorData;
    public Stopwatch stopwatch;
    
    public float waitTime = 2f;
    
    public Animator ElevatorAnims;

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    bool doOnce;

    void Start()
    {
        stopwatch = gameManager.GetComponent<Stopwatch>();
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
            if (!doOnce)
            {
                stopwatch.StartStopwatch();
                doOnce = true;
            }
            if (stopwatch.time >= 2)
            {
                ElevatorAnims.SetBool("GoUp", true);
                //WaitAndReset();
                ElevatorAnims.SetBool("GoDown", false);
                doOnce = false;
                elevatorData._goUp = false;
            }
        }
        else if (elevatorData._goDown)
        {
            if (!doOnce)
            {
                stopwatch.StartStopwatch();
                doOnce = true;
            }
            if (stopwatch.time >= 2)
            {
                ElevatorAnims.SetBool("GoDown", true);
                //WaitAndReset();
                ElevatorAnims.SetBool("GoUp", false);
                doOnce = false;
                elevatorData._goDown = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0, 0);
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
            StartCoroutine(WaitAndReset());
        }
        //ElevatorAnims.SetBool("GoUp", false);
        elevatorData._goUp = false;
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(2f);
    }

}
