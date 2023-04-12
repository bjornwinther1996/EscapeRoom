using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationSyncTest : MonoBehaviour
{
    public float _triggerValue;
    public float _previousTriggerValue;

    TriggerAnimationSync _triggerAnimationSync;

    private void Awake()
    {
        // Get a reference to the color sync component
        _triggerAnimationSync = GetComponent<TriggerAnimationSync>();
    }

    private void Update()
    {
        // If the color has changed (via the inspector), call SetColor on the color sync component.
        if (_triggerValue != _previousTriggerValue)
        {
            _triggerAnimationSync.SetTriggerValue(_triggerValue);
            _previousTriggerValue = _triggerValue;
        }

    }
}
