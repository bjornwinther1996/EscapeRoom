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
    };

    public int PlatformSequence;

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
        
    }

    public void InstantiatePlatforms()
    {
        platformArray = new GameObject[ColoumnLength, RowLength];
        for (int i = 0; i < ColoumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                platformArray[i, j] = (GameObject)Instantiate(PlatformPrefab, new Vector3(transform.position.x + i*ColoumnMultiplier, 0, transform.position.z+j*RowMultiplier), Quaternion.identity);
                platformArray[i, j].transform.parent = gameObject.transform; // set this.gamebojct as parent
            }
        }
    }

    public void SetRandomSequence(GameObject[,] arrayOfPlatforms) // the players start from the top and go down:
    {

        int randomChance = Random.Range(0, 4);
        bool[,] pathSequence;

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
                if (pathSequence[i,j] == true)
                {
                    arrayOfPlatforms[i, j].gameObject.GetComponent<Platform>().SetSolid(true);
                }
                else
                {
                    arrayOfPlatforms[i, j].gameObject.GetComponent<Platform>().SetSolid(false);
                }
            }
        }
    }
}
