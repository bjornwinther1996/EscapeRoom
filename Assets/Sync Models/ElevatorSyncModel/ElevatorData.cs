using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ElevatorData : MonoBehaviour
{

    public ElevatorSync _elevatorSync;
    private LeverData syncedLeverData;

    public bool _goUp = default;
    public bool _previousGoUp = default;

    public bool _goDown = default;
    public bool _previousGoDown = default;

    public GameObject GameManagerObj;
    private GameManagerData syncedGameVars;


    private void Awake()
    {
        _elevatorSync = GetComponent<ElevatorSync>();
        syncedLeverData = GetComponent<LeverData>();
        syncedGameVars = GameManagerObj.GetComponent<GameManagerData>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (syncedGameVars._level == 6)
        {
            _goDown = true;
            if(_goDown == true) { return; }
            syncedGameVars._level = 0;
        }

        if (_goUp != _previousGoUp)
        {
            _elevatorSync.SetGoUp(_goUp);
            _previousGoUp = _goUp;
        }

        if (_goDown != _previousGoDown)
        {
            _elevatorSync.SetGoDown(_goDown);
            _previousGoDown = _goDown;
        }
    }
}
