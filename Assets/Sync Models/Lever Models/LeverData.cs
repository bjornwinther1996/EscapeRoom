using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class LeverData : MonoBehaviour
{
    public LeverSync _leverSync;

    [SerializeField]
    public int _leversPulled = default;
    public int _previousLeversPulled = default;

    private void Awake()
    {
        _leverSync = GetComponent<LeverSync>();
    }

    private void Update()
    {
        if (_leversPulled != _previousLeversPulled)
        {
            _leverSync.SetLeversPulled(_leversPulled);
            _previousLeversPulled = _leversPulled;
        }
    }
}
