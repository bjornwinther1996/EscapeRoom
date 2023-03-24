using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlatformManager : MonoBehaviour
{

    public GameObject PlatformPrefab; // put realtimecomponents on platform. - Does this change the instantiate method?

    public const int ColoumnLength = 5;
    public const int RowLength = 7;
    public static int COLOUMNLENGTH; // static variables for platform class to grab
    public static int ROWLENGTH;
    public float ColoumnMultiplier = 0.73f; // Inspector values overwrite! Set in Inspector of PlatformGrid!!!
    public float RowMultiplier = 0.73f; // Inspector values overwrite! Set in Inspector!!!

    public GameObject[,] platformArray;

    // hardcorded path sequences. has to be manually adjusted according to ColoumnLength and RowLength
    
    private int[,] pathSequence1 = new int[ColoumnLength, RowLength] {          {0, 1, 0, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 2, 0, 0},

    };
    private int[,] pathSequence2 = new int[ColoumnLength, RowLength] {          {0, 0, 1, 0, 0, 2, 0},
                                                                                {0, 0, 1, 2, 0, 0, 0},
                                                                                {0, 0, 2, 1, 0, 0, 0},
                                                                                {0, 2, 0, 0, 1, 0, 0},
                                                                                {2, 0, 0, 0, 0, 1, 0},

    };
    private int[,] pathSequence3 = new int[ColoumnLength, RowLength] {          {1, 0, 0, 2, 0, 0, 0},
                                                                                {1, 0, 0, 0, 2, 0, 0},
                                                                                {0, 1, 0, 0, 0, 2, 0},
                                                                                {0, 1, 0, 0, 0, 0, 2},
                                                                                {0, 0, 1, 0, 0, 0, 2},

    };
    private int[,] pathSequence4 = new int[ColoumnLength, RowLength] {          {0, 0, 0, 1, 0, 0, 2},
                                                                                {0, 0, 1, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 0, 2},
                                                                                {0, 1, 0, 0, 0, 0, 2},

    };

    public int PlatformSequence;

    public int RowIndex = 1;
    public int PreviousRowIndex;

    public int NumOfPlatformsActivatedInRow;

    public GameObject FloorHeaven;

    public static bool isResetFinished = true;


    void Start()
    {
        COLOUMNLENGTH = ColoumnLength;
        ROWLENGTH = RowLength;
        //platformArray = new GameObject[ColoumnLength, RowLength];
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
                platformArray[i,j] = Realtime.Instantiate("PlatformV2", new Vector3(transform.position.x + i * ColoumnMultiplier, 0, transform.position.z + j * RowMultiplier), Quaternion.identity, new Realtime.InstantiateOptions
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
        platform.transform.position += new Vector3(-0.025f, 0, 0);
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
        Debug.Log("PrevRow = Row");
        PreviousRowIndex = RowIndex;
    }

    public void CheckCorrectPath(int rowToCheck) // Could limit to check only one row - Just as in ActivateNextRow
    {
        for (int i = 0; i < rowToCheck; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                if (platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().GetPlatformActivated())
                {
                    NumOfPlatformsActivatedInRow++;
                    platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SetPlatformActivated(false);
                    if (NumOfPlatformsActivatedInRow >= 2) // if 2 or more
                    {
                        RowIndex++;
                        NumOfPlatformsActivatedInRow = 0;
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

        int randomChance = Random.Range(0, 4);
        int[,] pathSequence;

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
                pathSequence = pathSequence4;
                PlatformSequence = 4;
                break;
            default:
                pathSequence = pathSequence1;
                PlatformSequence = 1;
                break;
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
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer1 = false;
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer2 = false;
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
        //FloorHeaven.transform.position += new Vector3(100, 0, 0);
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
        //FloorHeaven.transform.position += new Vector3(-100, 0, 0);
    }

    public IEnumerator SetRandomSequenceAfterXTime(float time) // the players start from the top and go down:
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Reset Sequence of Platforms METHOD");
        int randomChance = Random.Range(0, 4);
        int[,] pathSequence;

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
                pathSequence = pathSequence4;
                PlatformSequence = 4;
                break;
            default:
                pathSequence = pathSequence1;
                PlatformSequence = 1;
                break;
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
                else // so that i can call this method (SetrandomSequence) - to reset platforms according to new sequence.
                {
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer1 = false;
                    platformArray[i, j].gameObject.GetComponentInChildren<PlatformData>()._isSolidPlayer2 = false;
                }
            }
        }
    }

    public IEnumerator ResetPositionOfDisabledPlatforms(float time) // obsolete?
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Reset Position Of Disabled Platforms METHOD");
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                if (platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().GetPlatformDisabled())
                {
                    //SpawnPlatform(platformArray[i, j].transform.GetChild(0).gameObject);
                    //platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPlatform(); 
                    platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SetPlatformDisabled(false);
                }
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

    public void ResetActivatedPlatforms() // Resets all activated platforms
    {
        Debug.Log("Reset Activated Platforms METHOD");
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SetPlatformActivated(false);
            }
        }
        NumOfPlatformsActivatedInRow = 0;
        RowIndex = 1;
    }

    public IEnumerator ActivateNextRow(int rowToActivate, float time) // Make petter performance-wise so it doesnt continously activate components.
    {
        yield return new WaitForSeconds(time);
        for (int targetRow = rowToActivate - 1; targetRow < rowToActivate; targetRow++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                //SpawnPlatform(platformArray[targetRow, j].transform.GetChild(0).gameObject);
                //SetPosition(platformArray[targetRow, j].transform.GetChild(0).gameObject, platformArray[targetRow, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPosition);
                platformArray[targetRow, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SpawnPlatform();
                Debug.Log("Activate Next Row IEnumerator (AFTER FAIL)");
            }
        } 
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
