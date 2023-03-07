using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class GameManagerSyncModel {

    [RealtimeProperty(1, true, true)]
    private float _gameTime;

    [RealtimeProperty(2, true, true)]
    private float _gameScore;

    [RealtimeProperty(3, true, true)]
    private bool _isAllPlayersReady;

    [RealtimeProperty(4, true, true)]
    private int _sequenceIndex;

    [RealtimeProperty(5, true, true)]
    private int _pathSequence;

    [RealtimeProperty(6, true, true)]
    private int _fails;

    [RealtimeProperty(7, true, true)]
    private int _level;
    
    [RealtimeProperty(8, true, true)]
    private int _rowIndex;
    
    [RealtimeProperty(9, true, true)]
    private int _destroyedPlatforms;

    [RealtimeProperty(10, true, true)]
    private bool _backupBool;

    [RealtimeProperty(11, true, true)]
    private float _backupFloat;

    [RealtimeProperty(12, true, true)]
    private int _backupInt;

}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class GameManagerSyncModel : RealtimeModel {
    public float gameTime {
        get {
            return _gameTimeProperty.value;
        }
        set {
            if (_gameTimeProperty.value == value) return;
            _gameTimeProperty.value = value;
            InvalidateReliableLength();
            FireGameTimeDidChange(value);
        }
    }
    
    public float gameScore {
        get {
            return _gameScoreProperty.value;
        }
        set {
            if (_gameScoreProperty.value == value) return;
            _gameScoreProperty.value = value;
            InvalidateReliableLength();
            FireGameScoreDidChange(value);
        }
    }
    
    public bool isAllPlayersReady {
        get {
            return _isAllPlayersReadyProperty.value;
        }
        set {
            if (_isAllPlayersReadyProperty.value == value) return;
            _isAllPlayersReadyProperty.value = value;
            InvalidateReliableLength();
            FireIsAllPlayersReadyDidChange(value);
        }
    }
    
    public int sequenceIndex {
        get {
            return _sequenceIndexProperty.value;
        }
        set {
            if (_sequenceIndexProperty.value == value) return;
            _sequenceIndexProperty.value = value;
            InvalidateReliableLength();
            FireSequenceIndexDidChange(value);
        }
    }
    
    public int pathSequence {
        get {
            return _pathSequenceProperty.value;
        }
        set {
            if (_pathSequenceProperty.value == value) return;
            _pathSequenceProperty.value = value;
            InvalidateReliableLength();
            FirePathSequenceDidChange(value);
        }
    }
    
    public int fails {
        get {
            return _failsProperty.value;
        }
        set {
            if (_failsProperty.value == value) return;
            _failsProperty.value = value;
            InvalidateReliableLength();
            FireFailsDidChange(value);
        }
    }
    
    public int level {
        get {
            return _levelProperty.value;
        }
        set {
            if (_levelProperty.value == value) return;
            _levelProperty.value = value;
            InvalidateReliableLength();
            FireLevelDidChange(value);
        }
    }
    
    public int rowIndex {
        get {
            return _rowIndexProperty.value;
        }
        set {
            if (_rowIndexProperty.value == value) return;
            _rowIndexProperty.value = value;
            InvalidateReliableLength();
            FireRowIndexDidChange(value);
        }
    }
    
    public int destroyedPlatforms {
        get {
            return _destroyedPlatformsProperty.value;
        }
        set {
            if (_destroyedPlatformsProperty.value == value) return;
            _destroyedPlatformsProperty.value = value;
            InvalidateReliableLength();
            FireDestroyedPlatformsDidChange(value);
        }
    }
    
    public bool backupBool {
        get {
            return _backupBoolProperty.value;
        }
        set {
            if (_backupBoolProperty.value == value) return;
            _backupBoolProperty.value = value;
            InvalidateReliableLength();
            FireBackupBoolDidChange(value);
        }
    }
    
    public float backupFloat {
        get {
            return _backupFloatProperty.value;
        }
        set {
            if (_backupFloatProperty.value == value) return;
            _backupFloatProperty.value = value;
            InvalidateReliableLength();
            FireBackupFloatDidChange(value);
        }
    }
    
    public int backupInt {
        get {
            return _backupIntProperty.value;
        }
        set {
            if (_backupIntProperty.value == value) return;
            _backupIntProperty.value = value;
            InvalidateReliableLength();
            FireBackupIntDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(GameManagerSyncModel model, T value);
    public event PropertyChangedHandler<float> gameTimeDidChange;
    public event PropertyChangedHandler<float> gameScoreDidChange;
    public event PropertyChangedHandler<bool> isAllPlayersReadyDidChange;
    public event PropertyChangedHandler<int> sequenceIndexDidChange;
    public event PropertyChangedHandler<int> pathSequenceDidChange;
    public event PropertyChangedHandler<int> failsDidChange;
    public event PropertyChangedHandler<int> levelDidChange;
    public event PropertyChangedHandler<int> rowIndexDidChange;
    public event PropertyChangedHandler<int> destroyedPlatformsDidChange;
    public event PropertyChangedHandler<bool> backupBoolDidChange;
    public event PropertyChangedHandler<float> backupFloatDidChange;
    public event PropertyChangedHandler<int> backupIntDidChange;
    
    public enum PropertyID : uint {
        GameTime = 1,
        GameScore = 2,
        IsAllPlayersReady = 3,
        SequenceIndex = 4,
        PathSequence = 5,
        Fails = 6,
        Level = 7,
        RowIndex = 8,
        DestroyedPlatforms = 9,
        BackupBool = 10,
        BackupFloat = 11,
        BackupInt = 12,
    }
    
    #region Properties
    
    private ReliableProperty<float> _gameTimeProperty;
    
    private ReliableProperty<float> _gameScoreProperty;
    
    private ReliableProperty<bool> _isAllPlayersReadyProperty;
    
    private ReliableProperty<int> _sequenceIndexProperty;
    
    private ReliableProperty<int> _pathSequenceProperty;
    
    private ReliableProperty<int> _failsProperty;
    
    private ReliableProperty<int> _levelProperty;
    
    private ReliableProperty<int> _rowIndexProperty;
    
    private ReliableProperty<int> _destroyedPlatformsProperty;
    
    private ReliableProperty<bool> _backupBoolProperty;
    
    private ReliableProperty<float> _backupFloatProperty;
    
    private ReliableProperty<int> _backupIntProperty;
    
    #endregion
    
    public GameManagerSyncModel() : base(null) {
        _gameTimeProperty = new ReliableProperty<float>(1, _gameTime);
        _gameScoreProperty = new ReliableProperty<float>(2, _gameScore);
        _isAllPlayersReadyProperty = new ReliableProperty<bool>(3, _isAllPlayersReady);
        _sequenceIndexProperty = new ReliableProperty<int>(4, _sequenceIndex);
        _pathSequenceProperty = new ReliableProperty<int>(5, _pathSequence);
        _failsProperty = new ReliableProperty<int>(6, _fails);
        _levelProperty = new ReliableProperty<int>(7, _level);
        _rowIndexProperty = new ReliableProperty<int>(8, _rowIndex);
        _destroyedPlatformsProperty = new ReliableProperty<int>(9, _destroyedPlatforms);
        _backupBoolProperty = new ReliableProperty<bool>(10, _backupBool);
        _backupFloatProperty = new ReliableProperty<float>(11, _backupFloat);
        _backupIntProperty = new ReliableProperty<int>(12, _backupInt);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _gameTimeProperty.UnsubscribeCallback();
        _gameScoreProperty.UnsubscribeCallback();
        _isAllPlayersReadyProperty.UnsubscribeCallback();
        _sequenceIndexProperty.UnsubscribeCallback();
        _pathSequenceProperty.UnsubscribeCallback();
        _failsProperty.UnsubscribeCallback();
        _levelProperty.UnsubscribeCallback();
        _rowIndexProperty.UnsubscribeCallback();
        _destroyedPlatformsProperty.UnsubscribeCallback();
        _backupBoolProperty.UnsubscribeCallback();
        _backupFloatProperty.UnsubscribeCallback();
        _backupIntProperty.UnsubscribeCallback();
    }
    
    private void FireGameTimeDidChange(float value) {
        try {
            gameTimeDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireGameScoreDidChange(float value) {
        try {
            gameScoreDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireIsAllPlayersReadyDidChange(bool value) {
        try {
            isAllPlayersReadyDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireSequenceIndexDidChange(int value) {
        try {
            sequenceIndexDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FirePathSequenceDidChange(int value) {
        try {
            pathSequenceDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireFailsDidChange(int value) {
        try {
            failsDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireLevelDidChange(int value) {
        try {
            levelDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireRowIndexDidChange(int value) {
        try {
            rowIndexDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireDestroyedPlatformsDidChange(int value) {
        try {
            destroyedPlatformsDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireBackupBoolDidChange(bool value) {
        try {
            backupBoolDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireBackupFloatDidChange(float value) {
        try {
            backupFloatDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    private void FireBackupIntDidChange(int value) {
        try {
            backupIntDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _gameTimeProperty.WriteLength(context);
        length += _gameScoreProperty.WriteLength(context);
        length += _isAllPlayersReadyProperty.WriteLength(context);
        length += _sequenceIndexProperty.WriteLength(context);
        length += _pathSequenceProperty.WriteLength(context);
        length += _failsProperty.WriteLength(context);
        length += _levelProperty.WriteLength(context);
        length += _rowIndexProperty.WriteLength(context);
        length += _destroyedPlatformsProperty.WriteLength(context);
        length += _backupBoolProperty.WriteLength(context);
        length += _backupFloatProperty.WriteLength(context);
        length += _backupIntProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _gameTimeProperty.Write(stream, context);
        writes |= _gameScoreProperty.Write(stream, context);
        writes |= _isAllPlayersReadyProperty.Write(stream, context);
        writes |= _sequenceIndexProperty.Write(stream, context);
        writes |= _pathSequenceProperty.Write(stream, context);
        writes |= _failsProperty.Write(stream, context);
        writes |= _levelProperty.Write(stream, context);
        writes |= _rowIndexProperty.Write(stream, context);
        writes |= _destroyedPlatformsProperty.Write(stream, context);
        writes |= _backupBoolProperty.Write(stream, context);
        writes |= _backupFloatProperty.Write(stream, context);
        writes |= _backupIntProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.GameTime: {
                    changed = _gameTimeProperty.Read(stream, context);
                    if (changed) FireGameTimeDidChange(gameTime);
                    break;
                }
                case (uint) PropertyID.GameScore: {
                    changed = _gameScoreProperty.Read(stream, context);
                    if (changed) FireGameScoreDidChange(gameScore);
                    break;
                }
                case (uint) PropertyID.IsAllPlayersReady: {
                    changed = _isAllPlayersReadyProperty.Read(stream, context);
                    if (changed) FireIsAllPlayersReadyDidChange(isAllPlayersReady);
                    break;
                }
                case (uint) PropertyID.SequenceIndex: {
                    changed = _sequenceIndexProperty.Read(stream, context);
                    if (changed) FireSequenceIndexDidChange(sequenceIndex);
                    break;
                }
                case (uint) PropertyID.PathSequence: {
                    changed = _pathSequenceProperty.Read(stream, context);
                    if (changed) FirePathSequenceDidChange(pathSequence);
                    break;
                }
                case (uint) PropertyID.Fails: {
                    changed = _failsProperty.Read(stream, context);
                    if (changed) FireFailsDidChange(fails);
                    break;
                }
                case (uint) PropertyID.Level: {
                    changed = _levelProperty.Read(stream, context);
                    if (changed) FireLevelDidChange(level);
                    break;
                }
                case (uint) PropertyID.RowIndex: {
                    changed = _rowIndexProperty.Read(stream, context);
                    if (changed) FireRowIndexDidChange(rowIndex);
                    break;
                }
                case (uint) PropertyID.DestroyedPlatforms: {
                    changed = _destroyedPlatformsProperty.Read(stream, context);
                    if (changed) FireDestroyedPlatformsDidChange(destroyedPlatforms);
                    break;
                }
                case (uint) PropertyID.BackupBool: {
                    changed = _backupBoolProperty.Read(stream, context);
                    if (changed) FireBackupBoolDidChange(backupBool);
                    break;
                }
                case (uint) PropertyID.BackupFloat: {
                    changed = _backupFloatProperty.Read(stream, context);
                    if (changed) FireBackupFloatDidChange(backupFloat);
                    break;
                }
                case (uint) PropertyID.BackupInt: {
                    changed = _backupIntProperty.Read(stream, context);
                    if (changed) FireBackupIntDidChange(backupInt);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _gameTime = gameTime;
        _gameScore = gameScore;
        _isAllPlayersReady = isAllPlayersReady;
        _sequenceIndex = sequenceIndex;
        _pathSequence = pathSequence;
        _fails = fails;
        _level = level;
        _rowIndex = rowIndex;
        _destroyedPlatforms = destroyedPlatforms;
        _backupBool = backupBool;
        _backupFloat = backupFloat;
        _backupInt = backupInt;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
