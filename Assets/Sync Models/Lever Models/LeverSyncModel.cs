using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class LeverSyncModel
{
    [RealtimeProperty(1,true,true)]
    private int _leversPulled;

}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class LeverSyncModel : RealtimeModel {
    public int leversPulled {
        get {
            return _leversPulledProperty.value;
        }
        set {
            if (_leversPulledProperty.value == value) return;
            _leversPulledProperty.value = value;
            InvalidateReliableLength();
            FireLeversPulledDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(LeverSyncModel model, T value);
    public event PropertyChangedHandler<int> leversPulledDidChange;
    
    public enum PropertyID : uint {
        LeversPulled = 1,
    }
    
    #region Properties
    
    private ReliableProperty<int> _leversPulledProperty;
    
    #endregion
    
    public LeverSyncModel() : base(null) {
        _leversPulledProperty = new ReliableProperty<int>(1, _leversPulled);
    }
    
    protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent) {
        _leversPulledProperty.UnsubscribeCallback();
    }
    
    private void FireLeversPulledDidChange(int value) {
        try {
            leversPulledDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _leversPulledProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _leversPulledProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.LeversPulled: {
                    changed = _leversPulledProperty.Read(stream, context);
                    if (changed) FireLeversPulledDidChange(leversPulled);
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
        _leversPulled = leversPulled;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
