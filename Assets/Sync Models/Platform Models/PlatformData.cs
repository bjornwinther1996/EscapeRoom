using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlatformData : MonoBehaviour
{
    public PlatformSync _platformSync;

    [SerializeField]
    public bool _isSolidPlayer1 = default;
    public bool _previousIsSolidPlayer1 = default;

    [SerializeField]
    public bool _isSolidPlayer2 = default;
    public bool _previousIsSolidPlayer2 = default;

    [SerializeField]
    public bool _backupBool = default;
    public bool _previousBackupBool = default;

    [SerializeField]
    public float _backupFloat = default;
    public float _previousBackupFloat = default;

    [SerializeField]
    public int _backupInt = default;
    public int _previousBackupInt = default;


    private void Awake()
    {
        _platformSync = GetComponent<PlatformSync>();
    }

    private void Update()
    {
        if (_isSolidPlayer1 != _previousIsSolidPlayer1)
        {
            _platformSync.SetIsSolidPlayer1(_isSolidPlayer1);
            _previousIsSolidPlayer1 = _isSolidPlayer1;
        }

        if (_isSolidPlayer2 != _previousIsSolidPlayer2)
        {
            _platformSync.SetIsSolidPlayer2(_isSolidPlayer2);
            _previousIsSolidPlayer2 = _isSolidPlayer2;
        }

        if (_backupBool != _previousBackupBool)
        {
            _platformSync.SetBackupBool(_backupBool);
            _previousBackupBool = _backupBool;
        }

        if (_backupFloat != _previousBackupFloat)
        {
            _platformSync.SetBackupFloat(_backupFloat);
            _previousBackupFloat = _backupFloat;
        }

        if (_backupInt != _previousBackupInt)
        {
            _platformSync.SetBackupInt(_backupInt);
            _previousBackupInt = _backupInt;
        }
    }


}



