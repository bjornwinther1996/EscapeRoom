using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEditor;

public class ElevatorBehaviour : MonoBehaviour
{
    public Animator eleAnim;

    public bool openDoors = false;
    public bool closeDoors = false;

    public GameObject doorLeft;
    public GameObject doorRight;

    public ElevatorData elevatorData;
    public GameObject ChildObj;
    private AudioSource audioSource;
    public AudioClip ElevatorMusic;
    private float previousYPos = 2.086f;
    private bool isMusicPlaying;

    Vector3 startPos;
    [SerializeField]
    Vector3 endPos;


    // Start is called before the first frame update

    void Start()
    {
        // OLD BUT WORKS
        //startPos = transform.position;
        //endPos = transform.position + offset;
        eleAnim = GetComponent<Animator>();

        startPos = new Vector3(this.transform.position.x, 2.086f, this.transform.position.z);
        endPos = new Vector3(this.transform.position.x, -100.1f, this.transform.position.z);

        elevatorData = GetComponent<ElevatorData>();
        //gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
        audioSource = ChildObj.GetComponent<AudioSource>();
        //previousYPos = 2.086f;
    }

    // Update is called once per frame

    void Update()
    {

        if (GameManager.IsServer)
        {
            if (elevatorData._goUp)
            {
                gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                transform.position = Vector3.MoveTowards(transform.position, startPos, 8f * Time.deltaTime);
                //Debug.Log(Time.deltaTime);

                if (transform.position == startPos)
                {
                    gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                    transform.position = startPos;
                    elevatorData._goUp = false;
                    //eleAnim.SetBool("Open", true);
                }
            }

            if (elevatorData._goDown)
            {
                gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                transform.position = Vector3.MoveTowards(transform.position, endPos, 8f * Time.deltaTime);

                if (transform.position == endPos)
                {
                    gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                    transform.position = endPos;
                    elevatorData._goDown = false;
                    //eleAnim.SetBool("Open", true);
                }
            }
        }

        if (elevatorData._goUp)
        {
            eleAnim.SetBool("Open", false);
        }

        if (elevatorData._goDown)
        {
            eleAnim.SetBool("Open", false);
        }

        if (transform.position == startPos)
        {
            eleAnim.SetBool("Open", true);
        }
        
        if (transform.position == endPos)
        {
            eleAnim.SetBool("Open", true);
        }

        //The following is ran outside of servercheck, because it needs to run on both client and server:
        if (MovingUpwards() && !isMusicPlaying)
        {
            audioSource.PlayOneShot(ElevatorMusic);
            Debug.Log("MUSIC PLAYING");
            isMusicPlaying = true;
        }
        else if (transform.position.y == startPos.y && isMusicPlaying)
        {
            //audioSource.Stop();
            Debug.Log("Music Stopped!");
            audioSource.Stop();
            isMusicPlaying = false;
        }

    }

    public bool MovingUpwards()
    {
        if (previousYPos < transform.position.y)
        {
            //Debug.Log("True: Prev: " + previousYPos + "CurrentPos: " + transform.position.y);
            previousYPos = transform.position.y;
            return true;
        }
        else
        {
            previousYPos = transform.position.y;
            return false;
        }
    }

}


