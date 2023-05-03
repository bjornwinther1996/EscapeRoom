using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    private TextMeshPro textObj;

    public GameObject GameManagerObj;
    GameManagerData syncedGameManagerVars;
    int previousAmountOfLeversPulled = -1;
    public GameObject ElevatorObj;
    
    // Start is called before the first frame update
    void Start()
    {
        textObj = GetComponent<TextMeshPro>();
        syncedGameManagerVars = GameManagerObj.GetComponent<GameManagerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ElevatorObj.GetComponent<ElevatorData>()._goDown)
        {
            textObj.SetText("Elevator is coming!");
        }
        else if (syncedGameManagerVars._level != previousAmountOfLeversPulled)
        {
            textObj.SetText("Levers Pulled: " +syncedGameManagerVars._level);
            previousAmountOfLeversPulled = syncedGameManagerVars._level;
        }
    }
}
