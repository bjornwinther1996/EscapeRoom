using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class PlatformSync : RealtimeComponent<PlatformSyncModel>
{
    private PlatformData _platform;

    private void Awake()
    {
        _platform = GetComponent<PlatformData>();
    }

    protected override void OnRealtimeModelReplaced(PlatformSyncModel previousModel, PlatformSyncModel currentModel) {
        
        if (previousModel != null) {
            
            previousModel.isSolidPlayer1DidChange -= IsSolidPlayer1DidChange;
            previousModel.isSolidPlayer2DidChange -= IsSolidPlayer2DidChange;
            previousModel.backupBoolDidChange -= BackupBoolDidChange;
            previousModel.backupFloatDidChange -= BackupFloatDidChange;
            previousModel.backupIntDidChange -= BackupIntDidChange;

        }

        if (currentModel != null) {
            if (currentModel.isFreshModel) {
                currentModel.isSolidPlayer1 = _platform._isSolidPlayer1;
                currentModel.isSolidPlayer2 = _platform._isSolidPlayer2;
                currentModel.backupBool = _platform._backupBool;
                currentModel.backupFloat = _platform._backupFloat;
                currentModel.backupInt = _platform._backupInt;
            }

            UpdateIsSolidPlayer1();
            UpdateIsSolidPlayer2();
            UpdateBackupBool();
            UpdateBackupFloat();
            UpdateBackupInt();

            currentModel.isSolidPlayer1DidChange += IsSolidPlayer1DidChange;
            currentModel.isSolidPlayer2DidChange += IsSolidPlayer2DidChange;
            currentModel.backupBoolDidChange += BackupBoolDidChange;
            currentModel.backupFloatDidChange += BackupFloatDidChange;
            currentModel.backupIntDidChange += BackupIntDidChange;

        }    
    }

    // Register for changes

    private void IsSolidPlayer1DidChange(PlatformSyncModel model, bool value)
    {
        UpdateIsSolidPlayer1();
    }

    private void IsSolidPlayer2DidChange(PlatformSyncModel model, bool value)
    {
        UpdateIsSolidPlayer2();
    }

    private void BackupBoolDidChange(PlatformSyncModel model, bool value)
    {
        UpdateBackupBool();
    }

    private void BackupFloatDidChange(PlatformSyncModel model, float value)
    {
        UpdateBackupFloat();
    }

    private void BackupIntDidChange(PlatformSyncModel model, int value)
    {
        UpdateBackupInt();
    }

    // Update functions

    private void UpdateIsSolidPlayer1()
    {
        _platform._isSolidPlayer1 = model.isSolidPlayer1;
    }

    private void UpdateIsSolidPlayer2()
    {
        _platform._isSolidPlayer2 = model.isSolidPlayer2;
    }

    private void UpdateBackupBool()
    {
        _platform._backupBool = model.backupBool;
    }

    private void UpdateBackupFloat()
    {
        _platform._backupFloat = model.backupFloat;
    }

    private void UpdateBackupInt()
    {
        _platform._backupInt = model.backupInt;
    }

    // Getters and Setters

    public bool GetIsSolidPlayer1()
    {
        return model.isSolidPlayer1;
    }

    public void SetIsSolidPlayer1(bool value)
    {
        model.isSolidPlayer1 = value;
    }

    public bool GetIsSolidPlayer2()
    {
        return model.isSolidPlayer2;
    }

    public void SetIsSolidPlayer2(bool value)
    {
        model.isSolidPlayer2 = value;
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
