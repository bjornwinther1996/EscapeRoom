using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationSyncTest : MonoBehaviour
{
    //[SerializeField]
    public float _gripValue = default;
    private float _previousGripValue = default;

    //[SerializeField]
    public float _triggerValue = default;
    private float _previousTriggerValue = default;

    private HandAnimationSync _handAnimationSync;

    private void Awake()
    {
        // Get a reference to the color sync component
        _handAnimationSync = GetComponent<HandAnimationSync>();
    }

    private void Update()
    {
        // If the color has changed (via the inspector), call SetColor on the color sync component.
        if (_triggerValue != _previousTriggerValue)
        {
            _handAnimationSync.SetTriggerValue(_triggerValue);
            _previousTriggerValue = _triggerValue;
        }

        if (_gripValue != _previousGripValue)
        {
            _handAnimationSync.SetGripValue(_gripValue);
            _previousGripValue = _gripValue;
        }
    }
}
