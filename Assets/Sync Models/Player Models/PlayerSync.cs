using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlayerSync : RealtimeComponent<PlayerSyncModel>
{
    private PlayerData _player;

    private void Awake()
    {
        _player = GetComponent<PlayerData>();
    }

    protected override void OnRealtimeModelReplaced(PlayerSyncModel previousModel, PlayerSyncModel currentModel) {
        
        if (previousModel != null) {

            previousModel.isServerDidChange -= IsServerDidChange;
            previousModel.isReadyDidChange -= IsReadyDidChange;
            previousModel.pathSequenceDidChange -= PathSequenceDidChange;
            previousModel.backupBoolDidChange -= BackupBoolDidChange;
            previousModel.backupFloatDidChange -= BackupFloatDidChange;
            previousModel.backupIntDidChange -= BackupIntDidChange;
        
        }

        if (currentModel != null) {
            if (currentModel.isFreshModel) {
                currentModel.isServer = _player._isServer;
                currentModel.isReady = _player._isReady;
                currentModel.pathSequence = _player._pathSequence;
                currentModel.backupBool = _player._backupBool;
                currentModel.backupFloat = _player._backupFloat;
                currentModel.backupInt = _player._backupInt;
            }

            UpdateIsServer();
            UpdateIsReady();
            UpdatePathSequence();
            UpdateBackupBool();
            UpdateBackupFloat();
            UpdateBackupInt();

            currentModel.isServerDidChange += IsServerDidChange;
            currentModel.isReadyDidChange += IsReadyDidChange;
            currentModel.pathSequenceDidChange += PathSequenceDidChange;
            currentModel.backupBoolDidChange += BackupBoolDidChange;
            currentModel.backupFloatDidChange += BackupFloatDidChange;
            currentModel.backupIntDidChange += BackupIntDidChange;

        }
    }

    // Register for changes

    private void IsServerDidChange(PlayerSyncModel model, bool value)
    {
        UpdateIsServer();
    }

    private void IsReadyDidChange(PlayerSyncModel model, bool value)
    {
        UpdateIsReady();
    }

    private void PathSequenceDidChange(PlayerSyncModel model, int value)
    {
        UpdatePathSequence();
    }

    private void BackupBoolDidChange(PlayerSyncModel model, bool value)
    {
        UpdateBackupBool();
    }

    private void BackupFloatDidChange(PlayerSyncModel model, float value)
    {
        UpdateBackupFloat();
    }

    private void BackupIntDidChange(PlayerSyncModel model, int value)
    {
        UpdateBackupInt();
    }

    // Update functions

    private void UpdateIsServer()
    {
        _player._isServer = model.isServer;
    }

    private void UpdateIsReady()
    {
        _player._isReady = model.isReady;
    }

    private void UpdatePathSequence()
    {
        _player._pathSequence = model.pathSequence;
    }

    private void UpdateBackupBool()
    {
        _player._backupBool = model.backupBool;
    }

    private void UpdateBackupFloat()
    {
        _player._backupFloat = model.backupFloat;
    }

    private void UpdateBackupInt()
    {
        _player._backupInt = model.backupInt;
    }

    // Getters and Setters

    public bool GetIsServer()
    {
        return model.isServer;
    }

    public void SetIsServer(bool value)
    {
        model.isServer = value;
    }

    public bool GetIsReady()
    {
        return model.isReady;
    }

    public void SetIsReady(bool value)
    {
        model.isReady = value;
    }

    public int GetPathSequence()
    {
        return model.pathSequence;
    }

    public void SetPathSequence(int value)
    {
        model.pathSequence = value;
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
