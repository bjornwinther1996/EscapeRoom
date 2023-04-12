using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class HandAnimationSync : RealtimeComponent<HandAnimationSyncModel>
{
    //private MeshRenderer _meshRenderer;
    Animator _handAnimator;
    

    private void Awake()
    {
        // Get a reference to the mesh renderer
        _handAnimator = GetComponentInChildren<Animator>();

    }

    protected override void OnRealtimeModelReplaced(HandAnimationSyncModel previousModel, HandAnimationSyncModel currentModel)
    {
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.triggerValueDidChange -= TriggerValueDidChange;
            previousModel.gripValueDidChange -= GripValueDidChange;
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                currentModel.triggerValue = _handAnimator.GetFloat("Trigger");
                currentModel.gripValue = _handAnimator.GetFloat("Grip");

            // Update the mesh render to match the new model
            //UpdateMeshRendererColor();
            UpdateTriggerValue();
            UpdateGripValue();

            // Register for events so we'll know if the color changes later
            currentModel.triggerValueDidChange += TriggerValueDidChange;
            currentModel.gripValueDidChange += GripValueDidChange;
        }
    }

    private void TriggerValueDidChange(HandAnimationSyncModel model, float value)
    {
        // Update the mesh renderer
        UpdateTriggerValue();
    }

    private void GripValueDidChange(HandAnimationSyncModel model, float value)
    {
        // Update the mesh renderer
        UpdateGripValue();
    }

    private void UpdateTriggerValue()
    {
        // Get the color from the model and set it on the mesh renderer.
        //_meshRenderer.material.color = model.color;
        _handAnimator.SetFloat("Trigger", model.triggerValue);
        
    }

    private void UpdateGripValue()
    {
        // Get the color from the model and set it on the mesh renderer.
        //_meshRenderer.material.color = model.color;
        _handAnimator.SetFloat("Grip", model.gripValue);
    }

    public void SetGripValue(float val)
    {
        // Set the color on the model
        // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
        model.gripValue = val;
    }

    public void SetTriggerValue(float val)
    {
        model.triggerValue = val;
    }
}
