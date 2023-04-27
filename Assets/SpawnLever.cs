using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnLever : MonoBehaviour
{
    private bool runOnce = false;
    private float timer;

    private GameObject lever;
    private HingeJoint hj;
    public static int leverID;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (GameManager.IsServer)
        {
            if (!runOnce && timer > 2)
            {
                if (gameObject.tag == "inverseMount")
                {
                    lever = Realtime.Instantiate("Lever_back", transform.position, transform.rotation, new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
                }
                else if (gameObject.tag == "leftMount")
                {
                    lever = Realtime.Instantiate("Lever_left", transform.position, transform.rotation, new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
                }
                else if (gameObject.tag == "rightMount")
                {
                    lever = Realtime.Instantiate("Lever_right", transform.position, transform.rotation, new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.transform.rotation = Quaternion.Euler(0.5f, 0, 0);
                }
                else
                {
                    lever = Realtime.Instantiate("Lever_front", transform.position, transform.rotation, new Realtime.InstantiateOptions
                    {
                        ownedByClient = false,
                        preventOwnershipTakeover = false,
                        destroyWhenOwnerLeaves = false,
                        destroyWhenLastClientLeaves = true
                    });
                    lever.transform.rotation = Quaternion.Euler(-25f, 0, 0);
                }
                
                hj = lever.GetComponent<HingeJoint>();
                if (gameObject.tag == "inverseMount")
                {
                    hj.connectedAnchor = new Vector3(transform.position.x + 0.02f, transform.position.y + 0.01f, transform.position.z - 0.02f);
                }
                else if (gameObject.tag == "leftMount")
                {
                    hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z - 0.02f);
                }
                else if (gameObject.tag == "rightMount")
                {
                    hj.connectedAnchor = new Vector3(transform.position.x + 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
                }
                else
                {
                    hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
                }
            }
            
            //lever.transform.Rotate(90f, 0, 0, Space.Self);d

            /*if (gameObject.tag == "inverseMount") 
            {
                gameObject.GetComponent<RealtimeTransform>().RequestOwnership();
                lever.GetComponent<RealtimeTransform>().RequestOwnership();
                Debug.Log("WAS TRIGGERED DAWG");
                lever.transform.Rotate(0, -180f, 0);
            }
            */

            runOnce = true;
        }
        else
        {
            /*
            Debug.Log("Else triggered");
            if (!runOnce && timer > 4)
            {
                Debug.Log("Else triggered - Timer");
                if (gameObject.tag == "inverseMount")
                {
                    foreach (GameObject backLever in GameObject.FindGameObjectsWithTag("LeverBack"))
                    {
                        if (!backLever.GetComponent<LeverBehaviour>().IsReferenced)
                        {
                            hj = backLever.GetComponent<HingeJoint>();
                            hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
                            backLever.GetComponent<LeverBehaviour>().IsReferenced = true;
                        }
                    }
                }
                else if (gameObject.tag == "leftMount")
                {
                    foreach (GameObject leftLever in GameObject.FindGameObjectsWithTag("LeverLeft"))
                    {
                        if (!leftLever.GetComponent<LeverBehaviour>().IsReferenced)
                        {
                            hj = leftLever.GetComponent<HingeJoint>();
                            hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
                            leftLever.GetComponent<LeverBehaviour>().IsReferenced = true;
                        }
                    }
                }
                else if (gameObject.tag == "rightMount")
                {
                    foreach (GameObject rightLever in GameObject.FindGameObjectsWithTag("LeverRight"))
                    {
                        if (!rightLever.GetComponent<LeverBehaviour>().IsReferenced)
                        {
                            hj = rightLever.GetComponent<HingeJoint>();
                            hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
                            rightLever.GetComponent<LeverBehaviour>().IsReferenced = true;
                        }
                    }
                }
                else
                {
                    foreach (GameObject frontLever in GameObject.FindGameObjectsWithTag("LeverFront"))
                    {
                        Debug.Log("Front Levers array: " + GameObject.FindGameObjectsWithTag("LeverFront") + "Lever: " + frontLever);
                        if (!frontLever.GetComponent<LeverBehaviour>().IsReferenced)
                        {
                            hj = frontLever.GetComponent<HingeJoint>();
                            hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
                            frontLever.GetComponent<LeverBehaviour>().IsReferenced = true;
                        }
                    }
                    //hj = lever.GetComponent<HingeJoint>();
                    //hj.connectedAnchor = new Vector3(transform.position.x - 0.02f, transform.position.y + 0.01f, transform.position.z + 0.02f);
                }
                runOnce = true;
            }*/
        }

        //leverInv.transform.rotation = Quaternion.Euler(-45, 0, 0);

    }
}
