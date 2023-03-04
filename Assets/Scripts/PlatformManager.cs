using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    public GameObject PlatformPrefab;

    public const int ColoumnLength = 5;
    public const int RowLength = 7;
    public float ColoumnMultiplier = 0.66f;
    public float RowMultiplier = 0.66f;

    private GameObject[,] platformArray;
    private bool[,] pathSequence1;
    private bool[,] pathSequence2;
    private bool[,] pathSequence3;
    private int [] oneDimArrayEx = {0,1,2,3};

    public int PlatformSequence;

    //need to get relatime component possibly? - and put realtime components on prefab.

    // Start is called before the first frame update
    void Start()
    {
        InstantiatePlatforms();
        SetSequence(platformArray);

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

    public void SetSequence(GameObject[,] arrayOfPlatforms) // the players start from the top and go down:
    {
        pathSequence1 = new bool[ColoumnLength, RowLength] { {false, false, false, false, true , false, false},
                                                             {false, false, false, false, true , false, false},
                                                             {false, false, false, false, true , false, false},
                                                             {false, false, false, false, true , false, false},
                                                             {false, false, false, false, true , false, false},

        };
        pathSequence2 = new bool[ColoumnLength, RowLength] { {false, true , false, false, false, false, false},
                                                             {false, false, true , false, false, false, false},
                                                             {false, false, false, true , false, false, false},
                                                             {false, false, false, false, true , false, false},
                                                             {false, false, false, false, false, true , false},

        };
        pathSequence3 = new bool[ColoumnLength, RowLength] { {true , false, false, false, false, false, false},
                                                             {true , false, false, false, false, false, false},
                                                             {false, true , false, false, false, false, false},
                                                             {false, true , false, false, false, false, false},
                                                             {false, false, true , false, false, false, false},

        };

        int randomChance = Random.Range(0, 3);
        bool[,] pathSequence;

        switch (randomChance)
        {
            case 0:
                pathSequence = pathSequence1;
                PlatformSequence = 0;
                break;
            case 1:
                pathSequence = pathSequence2;
                PlatformSequence = 1;
                break;
            case 2:
                pathSequence = pathSequence3;
                PlatformSequence = 2;
                break;
            default:
                pathSequence = pathSequence1;
                PlatformSequence = 0;
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
            }
        }
    }
}
