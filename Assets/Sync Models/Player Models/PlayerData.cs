using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerData : MonoBehaviour
{
    public PlayerSync _playerSync;

    [SerializeField]
    public bool _isServer = default;
    public bool _previousIsServer = default;

    [SerializeField]
    public bool _isReady = default; // Replaces isRightGrabPressed
    public bool _previousIsReady = default;

    [SerializeField]
    public int _pathSequence = default;
    public int _previousPathSequence = default;

    [SerializeField]
    public bool _backupBool = default; // Replaces isLeftGrabPressed
    public bool _previousBackupBool = default;

    [SerializeField]
    public float _backupFloat = default;
    public float _previousBackupFloat = default;

    [SerializeField]
    public int _backupInt = default;
    public int _previousBackupInt = default;

    private void Awake()
    {
        _playerSync = GetComponent<PlayerSync>();
    }

    
    private void Update()
    {
     
        if (_isServer != _previousIsServer)
        {
            _playerSync.SetIsServer(_isServer);
            _previousIsServer = _isServer;
        }

        if (_isReady != _previousIsReady)
        {
            _playerSync.SetIsReady(_isReady);
            _previousIsReady = _isReady;
        }

        if (_pathSequence != _previousPathSequence)
        {
            _playerSync.SetPathSequence(_pathSequence);
            _previousPathSequence = _pathSequence;
        }

        if (_backupBool != _previousBackupBool)
        {
            _playerSync.SetBackupBool(_backupBool);
            _previousBackupBool = _backupBool;
        }

        if (_backupFloat != _previousBackupFloat)
        {
            _playerSync.SetBackupFloat(_backupFloat);
            _previousBackupFloat = _backupFloat;
        }

        if (_backupInt != _previousBackupInt)
        {
            _playerSync.SetBackupInt(_backupInt);
            _previousBackupInt = _backupInt;
        }

    }
}
