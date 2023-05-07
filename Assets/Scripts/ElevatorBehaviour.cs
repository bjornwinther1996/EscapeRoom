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
        gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
    }

    // Update is called once per frame

    void Update()
    {
        if (elevatorData._goUp)
        {
            eleAnim.SetBool("Open", false);
            gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
            transform.position = Vector3.MoveTowards(transform.position, startPos, 0.1f);

            if (transform.position == startPos)
            {
                gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                transform.position = startPos;
                elevatorData._goUp = false;
                eleAnim.SetBool("Open", true);
            }
        }

        if (elevatorData._goDown)
        {
            eleAnim.SetBool("Open", false);
            gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
            transform.position = Vector3.MoveTowards(transform.position, endPos, 0.1f);

            if (transform.position == endPos)
            {
                gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                transform.position = endPos;
                elevatorData._goDown = false;
                eleAnim.SetBool("Open", true);
            }
        }

    }
}


