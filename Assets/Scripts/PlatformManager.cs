using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlatformManager : MonoBehaviour
{

    public GameObject PlatformPrefab; // put realtimecomponents on platform. - Does this change the instantiate method?

    public const int ColoumnLength = 10;
    public const int RowLength = 7;
    public static int COLOUMNLENGTH; // static variables for platform class to grab
    public static int ROWLENGTH;
    private float coloumnMultiplier = 0.65f; // Inspector values overwrite! Set in Inspector of PlatformGrid!!! // <-- Changed to private now, so its only set via script
    private float rowMultiplier = 0.65f; // Inspector values overwrite! Set in Inspector!!! // <-- Changed to private now, so its only set via script

    public GameObject[,] platformArray;
    public GameObject AudioObj;

    // hardcorded path sequences. has to be manually adjusted according to ColoumnLength and RowLength

    //Shared space (Task):
    private int[,] pathSequence1 = new int[ColoumnLength, RowLength] {          {0, 0, 1, 0, 2, 0, 0},
                                                                                {0, 0, 1, 2, 0, 0, 0},
                                                                                {0, 0, 2, 1, 0, 0, 0},
                                                                                {0, 0, 2, 1, 0, 0, 0},
                                                                                {0, 2, 1, 0, 0, 0, 0},
                                                                                {0, 1, 2, 0, 0, 0, 0},
                                                                                {0, 1, 2, 0, 0, 0, 0},
                                                                                {0, 2, 1, 0, 0, 0, 0},
                                                                                {0, 2, 0, 1, 0, 0, 0},
                                                                                {0, 0, 2, 1, 0, 0, 0}

    };
    private int[,] pathSequence2 = new int[ColoumnLength, RowLength] {          {0, 0, 0, 1, 2, 0, 0},
                                                                                {0, 0, 0, 2, 1, 0, 0},
                                                                                {0, 0, 0, 2, 1, 0, 0},
                                                                                {0, 0, 0, 2, 1, 0, 0},
                                                                                {0, 0, 0, 0, 2, 1, 0},
                                                                                {0, 0, 0, 0, 1, 2, 0},
                                                                                {0, 0, 0, 1, 0, 2, 0},
                                                                                {0, 0, 1, 0, 2, 0, 0},
                                                                                {0, 0, 0, 1, 0, 2, 0},
                                                                                {0, 0, 0, 1, 2, 0, 0},

    };
    private int[,] pathSequence3 = new int[ColoumnLength, RowLength] {          {0, 0, 1, 2, 0, 0, 0},
                                                                                {0, 1, 0, 0, 2, 0, 0},
                                                                                {0, 0, 1, 2, 0, 0, 0},
                                                                                {0, 0, 2, 1, 0, 0, 0},
                                                                                {0, 0, 2, 1, 0, 0, 0},
                                                                                {0, 0, 1, 2, 0, 0, 0},
                                                                                {0, 0, 1, 0, 2, 0, 0},
                                                                                {0, 0, 0, 1, 2, 0, 0},
                                                                                {0, 0, 0, 2, 1, 0, 0},
                                                                                {0, 0, 2, 0, 1, 0, 0},

    };
    //Distributed space (Task)
    private int[,] pathSequence4 = new int[ColoumnLength, RowLength] {          {0, 0, 1, 0, 0, 2, 0},
                                                                                {0, 0, 1, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 2, 0},
                                                                                {1, 0, 0, 0, 2, 0, 0},
                                                                                {1, 0, 0, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 0, 2, 0},
                                                                                {0, 0, 1, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 2, 0},
                                                                                {1, 0, 0, 0, 2, 0, 0},

    };
    private int[,] pathSequence5 = new int[ColoumnLength, RowLength] {          {1, 0, 0, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 2, 0},
                                                                                {0, 0, 1, 0, 0, 2, 0},
                                                                                {0, 1, 0, 0, 2, 0, 0},
                                                                                {0, 0, 1, 0, 2, 0, 0},
                                                                                {0, 0, 0, 1, 0, 2, 0},
                                                                                {0, 0, 1, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 0, 2, 0},
                                                                                {0, 1, 0, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 0, 2},

    };
    private int[,] pathSequence6 = new int[ColoumnLength, RowLength] {          {0, 1, 0, 0, 2, 0, 0},
                                                                                {0, 0, 1, 0, 0, 2, 0},
                                                                                {0, 0, 0, 1, 0, 2, 0},
                                                                                {0, 0, 0, 0, 1, 0, 2},
                                                                                {0, 0, 0, 1, 0, 2, 0},
                                                                                {0, 0, 0, 1, 2, 0, 0},
                                                                                {0, 0, 1, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 0, 2, 0},
                                                                                {0, 1, 0, 0, 2, 0, 0},
                                                                                {1, 0, 0, 2, 0, 0, 0},

    };

    public int PlatformSequence;

    public int RowIndex = 1;
    public int PreviousRowIndex;

    public int NumOfPlatformsActivatedInRow;

    public GameObject FloorHeaven;
    private Vector3 floorHeavenStartPosition;
    private Vector3 floorHeavenOffsetPosition;

    public static bool isResetFinished = true;
    private float coloumnOffset = 2.03f;
    bool forcedIntoHell;


    void Start()
    {
        COLOUMNLENGTH = ColoumnLength;
        ROWLENGTH = RowLength;
        //platformArray = new GameObject[ColoumnLength, RowLength];
        floorHeavenStartPosition = FloorHeaven.transform.position;
        floorHeavenOffsetPosition = new Vector3(100, FloorHeaven.transform.position.y, FloorHeaven.transform.position.z);
    }

    // need to get relatime component possibly? - and put realtime components on prefab (already on!).
    // need to sync either the pathSequence, or the random int (randomChance) that determines the path sequence.
    // InstantiatePlatforms() and SetSequence() needs to be called from only one headset via gameManager? (first headset that connects - Master). ALso needs to be RealtimeInstantiate

    public void RealtimeInstantiatePlatforms()
    {
        platformArray = new GameObject[ColoumnLength, RowLength];
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                //GameObject platform = platformArray[i, j];
                platformArray[i,j] = Realtime.Instantiate("PlatformV2", new Vector3(transform.position.x + i * coloumnMultiplier -coloumnOffset, 0, transform.position.z + j * rowMultiplier), Quaternion.identity, new Realtime.InstantiateOptions
                {
                    ownedByClient = false, // True? 
                    preventOwnershipTakeover = false,
                    destroyWhenOwnerLeaves = false,
                    destroyWhenLastClientLeaves = true
                });
                //platformArray[i,j].GetComponent<RealtimeTransform>().RequestOwnership();
                MoveAndSetStartSpawnPosition(platformArray[i, j].transform.GetChild(0).gameObject); // Changed argument child obj instead of parent obj of Prefab.
                //platformArray[i,j].transform.SetParent(gameObject.transform); // ONLY SETS PARENT FOR SERVER!! THIS LINE ONLY RUNS FOR SERVER?!
                MoveAndSetDespawnPosition(platformArray[i, j].transform.GetChild(0).gameObject);
                Debug.Log("All Platforms Instantiated and Despawned");

            }
        }
    }

    public void MoveAndSetStartSpawnPosition(GameObject platform) // Temporary. Find a way to setparent for the spawned objects for both clients. Problem = needs to wait to setparent or instantiate obj untill both clients have connected
    {
        platform.GetComponent<RealtimeTransform>().RequestOwnership();
        //platform.transform.SetParent(gameObject.transform);
        //platform.transform.position += new Vector3(-2.02f, 0, 0); // created offset for coloumns when instantiated instead.
        platform.transform.position += new Vector3(-0.025f, 0, 0); //Redundant. Is set in coloumnOffset now when entire obj is instantiated.
        platform.GetComponent<Platform>().SpawnPosition = platform.transform.position;
        //Debug.Log("InitialPos: " + platform.GetComponent<Platform>().SpawnPosition);
    }

    public void MoveAndSetDespawnPosition(GameObject platform) // need to request ownership. Realtime.transform.requestOwnership()
    {
        platform.GetComponent<RealtimeTransform>().RequestOwnership();
        platform.transform.position += new Vector3(100, 0, 0);
        platform.GetComponent<Platform>().DespawnPosition = platform.transform.position;
    }

    public void ActivateNextRow(int rowToActivate) // Make petter performance-wise so it doesnt continously activate components.
    {
        //Debug.Log("ActivateNextRow Method triggered - RowToActivate(RowIndex):"  + rowToActivate);
        if(PreviousRowIndex == RowIndex) { return; } // maybe obsolete once called from GameManager. // needs to be in GameManager?
        
        for (int targetRow = rowToActivate -1; targetRow < rowToActivate; targetRow++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                //SpawnPlatform(platformArray[targetRow, j].transform.GetChild(0).gameObject);
                //SetPosition(platformArray[targetRow, j].transform.GetChild(0).gameObject, platformArray[targetRow, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPosition);
                platformArray[targetRow, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPlatform();
                Debug.Log("SpawnPlatform -100x");
            }
        }
        //Debug.Log("PrevRow = Row");
        if (RowIndex > 1) // Trigger Audio for each "New Row Call", except the first.
        {
            Realtime.Instantiate("RealtimeAudioObj", new Vector3(5, 0, 0), Quaternion.identity, new Realtime.InstantiateOptions
            {
                ownedByClient = false, // True? 
                preventOwnershipTakeover = false,
                destroyWhenOwnerLeaves = false,
                destroyWhenLastClientLeaves = true
            });
        }

        PreviousRowIndex = RowIndex;
    }

    public void CheckCorrectPath(int rowToCheck) // Could limit to check only one row - Just as in ActivateNextRow
    {
        //Debug.Log("Check Correct Path Method - Row to check: " + rowToCheck);
        for (int i = 0; i < rowToCheck; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                if (platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().GetPlatformActivated())
                {
                    NumOfPlatformsActivatedInRow++;
                    Debug.Log("NUmberOFPlatformsActivatedInRow INCREMENTED: " + NumOfPlatformsActivatedInRow);
                    platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SetPlatformActivated(false);
                    if (NumOfPlatformsActivatedInRow >= 2) // if 2 or more
                    {
                        RowIndex++;
                        NumOfPlatformsActivatedInRow = 0;
                        Debug.Log("CheckCorrectPath Method - Row Index increased! : " + RowIndex);
                    }
                    if (RowIndex > 4 && !forcedIntoHell) // Forced into hell:
                    {
                        //Forced into hell:
                        AudioObj.GetComponent<AudioSource>().Play();
                        StartCoroutine(ForcedIntoHell(5));
                        forcedIntoHell = true;
                    }
                }
            }
        }
    }

    /*public void SpawnPlatform(GameObject platform)
    {
        platform.GetComponent<RealtimeTransform>().RequestOwnership();
        platform.transform.position += new Vector3(-100, 0, 0);
    }*/

    /*
    public void SetPosition(GameObject platform, Vector3 position)
    {
        platform.GetComponent<RealtimeTransform>().RequestOwnership();
        platform.transform.position = position;
    }*/
    
    public void SetRandomSequence() // the players start from the top and go down:
    {
        int[,] pathSequence;
        if (GameManager.IsTaskSharedStatic)
        {
            int randomChance = Random.Range(0, 3);
            Debug.Log("Shared Task - Path: " + randomChance);
            switch (randomChance)
            {
                case 0:
                    pathSequence = pathSequence1;
                    PlatformSequence = 1;
                    break;
                case 1:
                    pathSequence = pathSequence2;
                    PlatformSequence = 2;
                    break;
                case 2:
                    pathSequence = pathSequence3;
                    PlatformSequence = 3;
                    break;
                case 3:
                default:
                    pathSequence = pathSequence1;
                    PlatformSequence = 1;
                    break;

            }
        }
        else
        {
            int randomChance = Random.Range(3, 6);
            Debug.Log("Distributed Task - Path: " + randomChance);
            switch (randomChance)
            {
                case 3:
                    pathSequence = pathSequence4;
                    PlatformSequence = 4;
                    break;
                case 4:
                    pathSequence = pathSequence5;
                    PlatformSequence = 5;
                    break;
                case 5:
                    pathSequence = pathSequence6;
                    PlatformSequence = 6;
                    break;
                default:
                    pathSequence = pathSequence5;
                    PlatformSequence = 5;
                    break;
            }
        }


        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                if (pathSequence[i,j] == 1)
                {
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer1 = true;
                    
                }
                else if (pathSequence[i, j] == 2)
                {
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer2 = true;
                }
                else // so that i can call this method (SetrandomSequence) - to reset platforms according to new sequence.
                {
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer1 = false; // Obsolete now as it is done in ResetPlatforms
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer2 = false; // Obsolete now as it is done in ResetPlatforms
                }
            }
        }
    }

    public void DestroyAllSurfaces()
    {
        Debug.Log("Destroy All Surfaces METHOD");
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().DespawnPlatform();
                //SetPosition(platformArray[i, j].transform.GetChild(0).gameObject, platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().DespawnPosition);
            }
        }
        FloorHeaven.GetComponent<RealtimeTransform>().RequestOwnership();
        FloorHeaven.transform.position = floorHeavenOffsetPosition;
        forcedIntoHell = true;
    }

    public IEnumerator EnableAllSurfaces(float time) // not used anymore
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Enable All SUrfaces Method");
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPlatform();
            }
        }
        FloorHeaven.GetComponent<RealtimeTransform>().RequestOwnership();
        FloorHeaven.transform.position = floorHeavenStartPosition;
    }

    public IEnumerator SetRandomSequenceAfterXTime(float time) // the players start from the top and go down:
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Reset Sequence of Platforms METHOD");
        //int randomChance = Random.Range(0, 6);
        int[,] pathSequence;

        if (GameManager.IsTaskSharedStatic)
        {
            int randomChance = Random.Range(0, 3);
            Debug.Log("Shared Task (Reset) - Path: " + randomChance);
            switch (randomChance)
            {
                case 0:
                    pathSequence = pathSequence1;
                    PlatformSequence = 1;
                    break;
                case 1:
                    pathSequence = pathSequence2;
                    PlatformSequence = 2;
                    break;
                case 2:
                    pathSequence = pathSequence3;
                    PlatformSequence = 3;
                    break;
                case 3:
                default:
                    pathSequence = pathSequence1;
                    PlatformSequence = 1;
                    break;

            }
        }
        else
        {
            int randomChance = Random.Range(3, 6);
            Debug.Log("Distributed Task (Reset) - Path: " + randomChance);
            switch (randomChance)
            {
                case 3:
                    pathSequence = pathSequence4;
                    PlatformSequence = 4;
                    break;
                case 4:
                    pathSequence = pathSequence5;
                    PlatformSequence = 5;
                    break;
                case 5:
                    pathSequence = pathSequence6;
                    PlatformSequence = 6;
                    break;
                default:
                    pathSequence = pathSequence5;
                    PlatformSequence = 5;
                    break;
            }
        }

        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                if (pathSequence[i, j] == 1)
                {
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer1 = true;

                }
                else if (pathSequence[i, j] == 2)
                {
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer2 = true;
                }
                else // so that i can call this method (SetrandomSequence) - to reset platforms according to new sequence. // wont work - Added code to account for this in ResetPlatforms
                {
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer1 = false; // Obsolete now as it is done in ResetPlatforms
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer2 = false; // Obsolete now as it is done in ResetPlatforms
                }
            }
        }
    }

    public IEnumerator ResetLocalPlatformVariables(float time) // obsolete?
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Reset local variables on all platforms");
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                    platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SetStopCalling(false); // stopCalling-var is used in Success-method in Platform   
            }
        }
    }

    public IEnumerator ResetMaterial(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Reset Material METHOD");
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().ResetMaterial();
            }
        }
        isResetFinished = true;
    }

    public void ResetAllPlatforms() // Resets all activated platforms
    {
        Debug.Log("Reset Activated Platforms METHOD");
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SetPlatformActivated(false);
                platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer1 = false;
                platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer2 = false;
            }
        }
        NumOfPlatformsActivatedInRow = 0;
        RowIndex = 1;
    }

    public IEnumerator ActivateNextRowIE(int rowToActivate, float time) // Make petter performance-wise so it doesnt continously activate components.
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Activate Next Row IEnumerator (AFTER FAIL)");
        for (int targetRow = rowToActivate - 1; targetRow < rowToActivate; targetRow++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                //SpawnPlatform(platformArray[targetRow, j].transform.GetChild(0).gameObject);
                //SetPosition(platformArray[targetRow, j].transform.GetChild(0).gameObject, platformArray[targetRow, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPosition);
                platformArray[targetRow, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPlatform();
            }
        }
        FloorHeaven.GetComponent<RealtimeTransform>().RequestOwnership();
        FloorHeaven.transform.position = floorHeavenStartPosition;
    }

    public IEnumerator ForcedIntoHell(float time)
    {
        yield return new WaitForSeconds(time);
        Platform.NumberOfPlatformsDestroyed = 1; // will trigger the reset for platforms in GameManager
    }

}


// Just saving temporarily in case we need it to be bool 2d array:
/*
private bool[,] pathSequence1 = new bool[ColoumnLength, RowLength] {        {false, false, false, false, true , false, false},
                                                                            {false, false, false, false, true , false, false},
                                                                            {false, false, false, false, true , false, false},
                                                                            {false, false, false, false, true , false, false},
                                                                            {false, false, false, false, true , false, false},

};
private bool[,] pathSequence2 = new bool[ColoumnLength, RowLength] {        {false, true , false, false, false, false, false},
                                                                            {false, false, true , false, false, false, false},
                                                                            {false, false, false, true , false, false, false},
                                                                            {false, false, false, false, true , false, false},
                                                                            {false, false, false, false, false, true , false},

};
private bool[,] pathSequence3 = new bool[ColoumnLength, RowLength] {        {true , false, false, false, false, false, false},
                                                                            {true , false, false, false, false, false, false},
                                                                            {false, true , false, false, false, false, false},
                                                                            {false, true , false, false, false, false, false},
                                                                            {false, false, true , false, false, false, false},

};
private bool[,] pathSequence4 = new bool[ColoumnLength, RowLength] {        {false, false, false, false, false, false, true },
                                                                            {false, false, false, false, false, false, true },
                                                                            {false, false, false, false, false, false, true },
                                                                            {false, false, false, false, false, false, true },
                                                                            {false, false, false, false, false, false, true }
};*/

/*
 
    // Start is called before the first frame update
    void Start()
    {

        //InstantiatePlatforms(); //call from gameManger where only Master-client calls it

        //SetRandomSequence(); //call from gameManger where only Master-client calls it

    }

    // Update is called once per frame
    void Update()
    {
        //ActivateNextRow(rowIndex);

        //CheckCorrectPath(rowIndex);
    }

    public void InstantiatePlatforms()
    {
        platformArray = new GameObject[ColoumnLength, RowLength];
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                //Below needs to be changed to RealtimeInstantiate - and to usethat, import package Using Normal.Realtime
                platformArray[i, j] = (GameObject)Instantiate(PlatformPrefab, new Vector3(transform.position.x + i * ColoumnMultiplier, 0, transform.position.z + j * RowMultiplier), Quaternion.identity);
                platformArray[i, j].transform.parent = gameObject.transform; // set this.gamebojct as parent
                                                                             //DisablePlatform(platformArray[i, j].transform.GetChild(0).gameObject);

            }
        }
    }

    public void DisablePlatform(GameObject platform) // doesnt work Realtime. Isn't synced
    {
        platform.GetComponent<MeshRenderer>().enabled = false;
        platform.GetComponent<Collider>().enabled = false;
    }

    public void EnablePlatform(GameObject platform) // doesnt work Realtime. Isn't synced
    {
        platform.GetComponent<MeshRenderer>().enabled = true;
        platform.GetComponent<Collider>().enabled = true;
    }



*/
