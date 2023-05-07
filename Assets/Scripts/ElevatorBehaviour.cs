using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ElevatorBehaviour : MonoBehaviour
{

    public bool openDoors = false;
    public bool closeDoors = false;

    public GameObject doorLeft;
    public GameObject doorRight;

    public ElevatorData elevatorData;

    Vector3 startPos;
    Vector3 endPos;

    Vector3 doorLeftStartPos;
    Vector3 doorRightStartPos;
    Vector3 doorLeftEndPos;
    Vector3 doorRightEndPos;

    float timer = 0;

    // Start is called before the first frame update

    void Start()
    {
        // OLD BUT WORKS
        //startPos = transform.position;
        //endPos = transform.position + offset;

        startPos = new Vector3(this.transform.position.x, 2.086f, this.transform.position.z);
        endPos = new Vector3(this.transform.position.x, -100.1f, this.transform.position.z);

        doorLeftEndPos = new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, doorLeft.transform.position.z - 0.75f);
        doorRightEndPos = new Vector3(doorRight.transform.position.x, doorRight.transform.position.y, doorRight.transform.position.z + 0.75f);

        doorRightStartPos = doorRight.transform.position;
        doorLeftStartPos = doorLeft.transform.position;

        elevatorData = GetComponent<ElevatorData>();
        gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
    }



    // Update is called once per frame

    void Update()
    {

        MoveUp();
        MoveDown();
        OpenDoors();
        CloseDoors();

        //transform.position = Vector3.Lerp(startPos, endPos, timer / 25);
        //timer += Time.deltaTime;

    }

    void MoveUp()
    {
        if (elevatorData._goUp)
        {
            if (transform.position != startPos)
            {
                transform.position = Vector3.Lerp(endPos, startPos, timer / 25);
                timer += Time.deltaTime;
            }
            else if (transform.position == startPos) { }
            {
                elevatorData._goUp = false;
                timer = 0;
            }
            
            
        }
    }

    private void MoveDown()
    {
        if (elevatorData._goDown)
        {
            if (transform.position != endPos)
            {
                transform.position = Vector3.Lerp(startPos, endPos, timer / 25);
                timer += Time.deltaTime;
            }
            /*else if (transform.position == endPos)
            {
                elevatorData._goDown = false;
            }
            */
            
        }
    }

    private void OpenDoors()
    {
        //Vector3 doorLeftEndPos = new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, doorLeft.transform.position.z);
        //doorLeftEndPos.z += 2;

        if (openDoors && !closeDoors)
        {
            if (doorLeft.transform.position != doorLeftEndPos && doorRight.transform.position != doorRightEndPos)
            {
                doorLeft.transform.position = Vector3.Lerp(doorLeft.transform.position, doorLeftEndPos, timer / 50);
                doorRight.transform.position = Vector3.Lerp(doorRight.transform.position, doorRightEndPos, timer / 50);
                timer += Time.deltaTime;
            }
            else
            {
                openDoors = false;
                timer = 0;
            }
            
        }
       
    }

    private void CloseDoors()
    {
        //Vector3 doorLeftEndPos = new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, doorLeft.transform.position.z);
        //doorLeftEndPos.z += 2;

        if (closeDoors && !openDoors)
        {
            if (doorLeft.transform.position != doorLeftStartPos && doorRight.transform.position != doorRightStartPos)
            {
                doorLeft.transform.position = Vector3.Lerp(doorLeft.transform.position, doorLeftStartPos, timer / 50);
                doorRight.transform.position = Vector3.Lerp(doorRight.transform.position, doorRightStartPos, timer / 50);
                timer += Time.deltaTime;
            }
            else
            {
                closeDoors = false;
                timer = 0;
            }

        }

    }

}


    /*
    public ElevatorData elevatorData;

    public Vector3 startPos;
    public Vector3 endPos;
    public float step;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(this.transform.position.x, 2.086f, this.transform.position.z);
        endPos = new Vector3(this.transform.position.x, -100.1f, this.transform.position.z);

        elevatorData = GetComponent<ElevatorData>();

        gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
    }

    // Update is called once per frame
    void Update()
    {
        //GoUp();
        //GoDown();

        //this.transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime);
        LerpPosition(endPos, -100);

    }

    
    private void GoUp()
    {
        if (elevatorData._goUp && this.transform.position != startPos)
        {
            gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
            this.transform.position = Vector3.Lerp(endPos, startPos, Time.deltaTime);

            if (this.transform.position == startPos)
            {
                elevatorData._goUp = false;
            }
        }
    }

    private void GoDown()
    {
        if (elevatorData._goDown && this.transform.position != endPos)
        {
            gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
            this.transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime);

            if (this.transform.position == endPos)
            {
                elevatorData._goDown = false;
            }
        }
    } 
    
    private void LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = startPos;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
        }
        //transform.position = targetPosition;
    }
    
}
*/
