using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class LeverIdSyncModel
{ 
    [RealtimeProperty(1, true, true)]
    private int _leverId;
}


/* ----- Begin Normal Autogenerated Code ----- */
public partial class LeverIdSyncModel : RealtimeModel {
    public int leverId {
        get {
            return _leverIdProperty.value;
        }
        set {
            if (_leverIdProperty.value == value) return;
            _leverIdProperty.value = value;
            InvalidateReliableLength();
            FireLeverIdDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(LeverIdSyncModel model, T value);
    public event PropertyChangedHandler<int> leverIdDidChange;
    
    public enum PropertyID : uint {
        LeverId = 1,
    }
    
    #region Properties
    
    private ReliableProperty<int> _leverIdProperty;
    
    #endregion
    
    public LeverIdSyncModel() : base(null) {
        _leverIdProperty = new ReliableProperty<int>(1, _leverId);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _leverIdProperty.UnsubscribeCallback();
    }
    
    private void FireLeverIdDidChange(int value) {
        try {
            leverIdDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _leverIdProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _leverIdProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.LeverId: {
                    changed = _leverIdProperty.Read(stream, context);
                    if (changed) FireLeverIdDidChange(leverId);
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
        _leverId = leverId;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */