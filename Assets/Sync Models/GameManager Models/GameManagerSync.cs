using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameManagerSync : RealtimeComponent<GameManagerSyncModel> 
{
    private GameManagerData _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManagerData>();
    }

    protected override void OnRealtimeModelReplaced(GameManagerSyncModel previousModel, GameManagerSyncModel currentModel) {

        if (previousModel != null) {

            previousModel.gameTimeDidChange -= GameTimeDidChange;
            previousModel.gameScoreDidChange -= GameScoreDidChange;
            previousModel.isAllPlayersReadyDidChange -= IsAllPlayersReadyDidChange;
            previousModel.sequenceIndexDidChange -= SequenceIndexDidChange;
            previousModel.pathSequenceDidChange -= PathSequenceDidChange;
            previousModel.failsDidChange -= FailsDidChange;
            previousModel.levelDidChange -= LevelDidChange;
            previousModel.rowIndexDidChange -= RowIndexDidChange;
            previousModel.backupBoolDidChange -= BackupBoolDidChange;
            previousModel.backupFloatDidChange -= BackupFloatDidChange;
            previousModel.backupIntDidChange -= BackupIntDidChange;

        }

        if (currentModel != null) {
            if (currentModel.isFreshModel) {
                currentModel.gameTime = _gameManager._gameTime;
                currentModel.gameScore = _gameManager._gameScore;
                currentModel.isAllPlayersReady = _gameManager._isAllPlayersReady;
                currentModel.sequenceIndex = _gameManager._sequenceIndex;
                currentModel.pathSequence = _gameManager._pathSequence;
                currentModel.fails = _gameManager._fails;
                currentModel.level = _gameManager._level;
                currentModel.rowIndex = _gameManager._rowIndex;
                currentModel.backupBool = _gameManager._backupBool;
                currentModel.backupFloat = _gameManager._backupFloat;
                currentModel.backupInt = _gameManager._backupInt;
            }

            UpdateGameTime();
            UpdateGameScore();
            UpdateIsAllPlayersReady();
            UpdateSequenceIndex();
            UpdatePathSequence();
            UpdateFails();
            UpdateLevel();
            UpdateRowIndex();
            UpdateBackupBool();
            UpdateBackupFloat();
            UpdateBackupInt();

            currentModel.gameTimeDidChange += GameTimeDidChange;
            currentModel.gameScoreDidChange += GameScoreDidChange;
            currentModel.isAllPlayersReadyDidChange += IsAllPlayersReadyDidChange;
            currentModel.sequenceIndexDidChange += SequenceIndexDidChange;
            currentModel.pathSequenceDidChange += PathSequenceDidChange;
            currentModel.failsDidChange += FailsDidChange;
            currentModel.levelDidChange += LevelDidChange;
            currentModel.rowIndexDidChange += RowIndexDidChange;
            currentModel.backupBoolDidChange += BackupBoolDidChange;
            currentModel.backupFloatDidChange += BackupFloatDidChange;
            currentModel.backupIntDidChange += BackupIntDidChange;

        }
    }

    // Register for changes

    private void GameTimeDidChange(GameManagerSyncModel model, float value)
    {
        UpdateGameTime();
    }

    private void GameScoreDidChange(GameManagerSyncModel model, float value)
    {
        UpdateGameScore();
    }

    private void IsAllPlayersReadyDidChange(GameManagerSyncModel model, bool value)
    {
        UpdateIsAllPlayersReady();
    }

    private void SequenceIndexDidChange(GameManagerSyncModel model, int value)
    {
        UpdateSequenceIndex();
    }

    private void PathSequenceDidChange(GameManagerSyncModel model, int value)
    {
        UpdatePathSequence();
    }

    private void FailsDidChange(GameManagerSyncModel model, int value)
    {
        UpdateFails();
    }

    private void LevelDidChange(GameManagerSyncModel model, int value)
    {
        UpdateLevel();
    }

    private void RowIndexDidChange(GameManagerSyncModel model, int value)
    {
        UpdateRowIndex();
    }

    private void BackupBoolDidChange(GameManagerSyncModel model, bool value)
    {
        UpdateBackupBool();
    }

    private void BackupFloatDidChange(GameManagerSyncModel model, float value)
    {
        UpdateBackupFloat();
    }

    private void BackupIntDidChange(GameManagerSyncModel model, int value)
    {
        UpdateBackupInt();
    }

    // Update functions

    private void UpdateGameTime()
    {
        _gameManager._gameTime = model.gameTime;
    }

    private void UpdateGameScore()
    {
        _gameManager._gameScore = model.gameScore;
    }

    private void UpdateIsAllPlayersReady()
    {
        _gameManager._isAllPlayersReady = model.isAllPlayersReady;
    }

    private void UpdateSequenceIndex()
    {
        _gameManager._sequenceIndex = model.sequenceIndex;
    }

    private void UpdatePathSequence()
    {
        _gameManager._pathSequence = model.pathSequence;
    }

    private void UpdateFails()
    {
        _gameManager._fails = model.fails;
    }

    private void UpdateLevel()
    {
        _gameManager._level = model.level;
    }

    private void UpdateRowIndex()
    {
        _gameManager._rowIndex = model.rowIndex;
    }

    private void UpdateBackupBool()
    {
        _gameManager._backupBool = model.backupBool;
    }

    private void UpdateBackupFloat()
    {
        _gameManager._backupFloat = model.backupFloat;
    }

    private void UpdateBackupInt()
    {
        _gameManager._backupInt = model.backupInt;
    }

    // Getters and Setters

    public float GetGameTime()
    {
        return model.gameTime;
    }

    public void SetGameTime(float value)
    {
        model.gameTime = value;
    }

    public float GetGameScore()
    {
        return model.gameScore;
    }

    public void SetGameScore(float value)
    {
        model.gameScore = value;
    }

    public bool GetIsAllPlayersReady()
    {
        return model.isAllPlayersReady;
    }

    public void SetIsAllPlayersReady(bool value)
    {
        model.isAllPlayersReady = value;
    }

    public int GetSequenceIndex()
    {
        return model.sequenceIndex;
    }

    public void SetSequenceIndex(int value)
    {
        model.sequenceIndex = value;
    }

    public int GetPathSequence()
    {
        return model.pathSequence;
    }

    public void SetPathSequence(int value)
    {
        model.pathSequence = value;
    }

    public int GetFails()
    {
        return model.fails;
    }

    public void SetFails(int value)
    {
        model.fails = value;
    }

    public int GetLevel()
    {
        return model.level;
    }

    public void SetLevel(int value)
    {
        model.level = value;
    }

    public int GetRowIndex()
    {
        return model.rowIndex;
    }

    public void SetRowIndex(int value)
    {
        model.rowIndex = value;
    }

    public bool GetBackupBool()
    {
        return model.backupBool;
    }

    public void SetBackupBool(bool value)
    {
        model.backupBool = value;
    }

    public float GetBackupFloat()
    {
        return model.backupFloat;
    }

    public void SetBackupFloat(float value)
    {
        model.backupFloat = value;
    }

    public int GetBackupInt()
    {
        return model.backupInt;
    }

    public void SetBackupInt(int value)
    {
        model.backupInt = value;
    }

}
