using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    public GameObject PlatformPrefab; // put realtimecomponents on platform. - Does this change the instantiate method?

    public const int ColoumnLength = 5;
    public const int RowLength = 7;
    public float ColoumnMultiplier = 0.73f; // Inspector values overwrite! Set in Inspector of PlatformGrid!!!
    public float RowMultiplier = 0.73f; // Inspector values overwrite! Set in Inspector!!!

    private GameObject[,] platformArray;

    // hardcorded path sequences. has to be manually adjusted according to ColoumnLength and RowLength
    
    private int[,] pathSequence1 = new int[ColoumnLength, RowLength] {          {0, 0, 0, 0, 1, 0, 0},
                                                                                {0, 0, 0, 0, 1, 0, 0},
                                                                                {0, 0, 0, 0, 1, 0, 0},
                                                                                {0, 0, 0, 0, 1, 0, 0},
                                                                                {0, 0, 0, 0, 1, 0, 0},

    };
    private int[,] pathSequence2 = new int[ColoumnLength, RowLength] {          {0, 1, 0, 0, 0, 0, 0},
                                                                                {0, 0, 1, 0, 0, 0, 0},
                                                                                {0, 0, 0, 1, 0, 0, 0},
                                                                                {0, 0, 0, 0, 1, 0, 0},
                                                                                {0, 0, 0, 0, 0, 1, 0},

    };
    private int[,] pathSequence3 = new int[ColoumnLength, RowLength] {          {1, 0, 0, 0, 0, 0, 0},
                                                                                {1, 0, 0, 0, 0, 0, 0},
                                                                                {0, 1, 0, 0, 0, 0, 0},
                                                                                {0, 1, 0, 0, 0, 0, 0},
                                                                                {0, 0, 1, 0, 0, 0, 0},

    };
    private int[,] pathSequence4 = new int[ColoumnLength, RowLength] {          {0, 0, 0, 0, 0, 0, 1},
                                                                                {0, 0, 0, 0, 0, 0, 1},
                                                                                {0, 0, 0, 0, 0, 0, 1},
                                                                                {0, 0, 0, 0, 0, 0, 1},
                                                                                {0, 0, 0, 0, 0, 0, 1},

    };

    public int PlatformSequence;

    private int rowIndex = 1;
    private int previousRowIndex;

    //need to get relatime component possibly? - and put realtime components on prefab.
    //need to sync either the pathSequence, or the random int (randomChance) that determines the path sequence.
    //InstantiatePlatforms() and SetSequence() needs to be called from only one headset via gameManager? (first headset that connects - Master) - will solve above problem as well

    // Start is called before the first frame update
    void Start()
    {

        InstantiatePlatforms(); //call from gameManger where only Master-client calls it

        SetRandomSequence(platformArray); //call from gameManger where only Master-client calls it

    }

    // Update is called once per frame
    void Update()
    {
        ActivateNextRow(rowIndex);

        CheckCorrectPath(rowIndex);
    }


    // ALL THE METHODS IN START AND UPDATE (IN THIS CLASS) ARE PROBABLY GOING TO BE CALLED FROM GAMEMANAGER AND NOT IN START/UDPATE!

    public void InstantiatePlatforms()
    {
        platformArray = new GameObject[ColoumnLength, RowLength];
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                platformArray[i, j] = (GameObject)Instantiate(PlatformPrefab, new Vector3(transform.position.x + i*ColoumnMultiplier, 0, transform.position.z+j*RowMultiplier), Quaternion.identity);
                platformArray[i, j].transform.parent = gameObject.transform; // set this.gamebojct as parent
                DisablePlatform(platformArray[i, j].transform.GetChild(0).gameObject);

            }
        }
    }

    public void ActivateNextRow(int rowToActivate) // Make petter performance-wise so it doesnt continously activate components.
    {
        if(previousRowIndex == rowIndex) { return; }; // maybe obsolete once called from GameManager. // needs to be in GameManager?
        for (int i = 0; i < rowToActivate; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                EnablePlatform(platformArray[i, j].transform.GetChild(0).gameObject);
            }
        }
        previousRowIndex = rowIndex;
    }

    public void CheckCorrectPath(int rowToCheck)
    { 
        for (int i = 0; i < rowToCheck; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                if (platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().GetPlatformActivated())
                {
                    rowIndex++;
                    platformArray[i, j].transform.GetChild(0).gameObject.GetComponent<Platform>().SetPlatformActivated(false);
                }
            }
        }
    }

    public void DisablePlatform(GameObject platform)
    {
        platform.GetComponent<MeshRenderer>().enabled = false;
        platform.GetComponent<Collider>().enabled = false;
    }
    public void EnablePlatform(GameObject platform)
    {
        platform.GetComponent<MeshRenderer>().enabled = true;
        platform.GetComponent<Collider>().enabled = true;
    }

    public void SetRandomSequence(GameObject[,] arrayOfPlatforms) // the players start from the top and go down:
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
                    arrayOfPlatforms[i, j].gameObject.GetComponentInChildren<Platform>().SetSolid(true);
                }
                else
                {
                    arrayOfPlatforms[i, j].gameObject.GetComponentInChildren<Platform>().SetSolid(false);
                }
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
