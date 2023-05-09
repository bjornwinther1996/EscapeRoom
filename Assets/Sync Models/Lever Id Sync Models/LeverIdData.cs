using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverIdData : MonoBehaviour
{
    public LeverIdSync _leverIdSync;

    [SerializeField]
    public int _leverId = default;
    public int _previousLeverId = default;

    private void Awake()
    {
        _leverIdSync = GetComponent<LeverIdSync>();
    }

    private void Update()
    {
        if (_leverId != _previousLeverId)
        {
            _leverIdSync.SetLeverId(_leverId);
            _previousLeverId = _leverId;
        }
    }
}
