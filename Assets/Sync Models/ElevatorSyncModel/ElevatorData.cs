using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ElevatorData : MonoBehaviour
{

    public ElevatorSync _elevatorSync;

    public bool _goUp = default;
    public bool _previousGoUp = default;

    public bool _goDown = default;
    public bool _previousGoDown = default;


    private void Awake()
    {
        _elevatorSync = GetComponent<ElevatorSync>();
    }

    // Update is called once per frame
    private void Update()
    {
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
