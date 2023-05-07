using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ElevatorBehaviour : MonoBehaviour
{

    public ElevatorData elevatorData;

    Vector3 startPos;
    Vector3 endPos;

    float timer = 0;

    // Start is called before the first frame update

    void Start()
    {
        // OLD BUT WORKS
        //startPos = transform.position;
        //endPos = transform.position + offset;

        startPos = new Vector3(this.transform.position.x, 2.086f, this.transform.position.z);
        endPos = new Vector3(this.transform.position.x, -100.1f, this.transform.position.z);

        elevatorData = GetComponent<ElevatorData>();
        gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
    }



    // Update is called once per frame

    void Update()
    {

        MoveUp();
        MoveDown();

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
            else
            {
                elevatorData._goUp = false;
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
            else
            {
                elevatorData._goDown = false;
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
