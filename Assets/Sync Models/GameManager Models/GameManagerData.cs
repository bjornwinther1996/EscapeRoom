using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameManagerData : MonoBehaviour
{
    public GameManagerSync _gameManagerSync;

    [SerializeField]
    public float _gameTime = default;
    public float _previousGameTime = default;

    [SerializeField]
    public float _gameScore = default;
    public float _previousGameScore = default;

    [SerializeField]
    public bool _isAllPlayersReady = default;
    public bool _previousIsAllPlayersReady = default;

    [SerializeField]
    public int _sequenceIndex = default;
    public int _previousSequenceIndex = default;

    [SerializeField]
    public int _pathSequence = default;
    public int _previousPathSequence = default;

    [SerializeField]
    public int _fails = default;
    public int _previousFails = default;

    [SerializeField]
    public int _level = default;
    public int _previousLevel = default;

    [SerializeField]
    public int _rowIndex = default;
    public int _previousRowIndex = default;

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
        _gameManagerSync = GetComponent<GameManagerSync>();
    }

    private void Update()
    {
        
        if (_gameTime != _previousGameTime)
        {
            _gameManagerSync.SetGameTime(_gameTime);
            _previousGameTime = _gameTime;
        }

        if (_gameScore != _previousGameScore)
        {
            _gameManagerSync.SetGameScore(_gameScore);
            _previousGameScore = _gameScore;
        }

        if (_isAllPlayersReady != _previousIsAllPlayersReady)
        {
            _gameManagerSync.SetIsAllPlayersReady(_isAllPlayersReady);
            _previousIsAllPlayersReady = _isAllPlayersReady;
        }

        if (_sequenceIndex != _previousSequenceIndex)
        {
            _gameManagerSync.SetSequenceIndex(_sequenceIndex);
            _previousSequenceIndex = _sequenceIndex;
        }

        if (_pathSequence != _previousPathSequence)
        {
            _gameManagerSync.SetPathSequence(_pathSequence);
            _previousPathSequence = _pathSequence;
        }

        if (_fails != _previousFails)
        {
            _gameManagerSync.SetFails(_fails);
            _previousFails = _fails;
        }

        if (_level != _previousLevel)
        {
            _gameManagerSync.SetLevel(_level);
            _previousLevel = _level;
        }

        if (_rowIndex != _previousRowIndex)
        {
            _gameManagerSync.SetRowIndex(_rowIndex);
            _previousRowIndex = _rowIndex;
        }

        if (_backupBool != _previousBackupBool)
        {
            _gameManagerSync.SetBackupBool(_backupBool);
            _previousBackupBool = _backupBool;
        }

        if (_backupFloat != _previousBackupFloat)
        {
            _gameManagerSync.SetBackupFloat(_backupFloat);
            _previousBackupFloat = _backupFloat;
        }

        if (_backupInt != _previousBackupInt)
        {
            _gameManagerSync.SetBackupInt(_backupInt);
            _previousBackupInt = _backupInt;
        }


    }
}
