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

    //need to get relatime component possibly? - and put realtime components on prefab.

    // Start is called before the first frame update
    void Start()
    {
        InstantiatePlatforms();
        setSequence();

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

    public void setSequence() // the players start from the top and go down:
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

    }
}
