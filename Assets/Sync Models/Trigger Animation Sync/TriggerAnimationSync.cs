using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class TriggerAnimationSync : RealtimeComponent<TriggerAnimationSyncModel>
{

    Animator _triggerAnimator;

    private void Awake()
    {
        // Get a reference to the mesh renderer
        _triggerAnimator = GetComponent<Animator>();

    }

    protected override void OnRealtimeModelReplaced(TriggerAnimationSyncModel previousModel, TriggerAnimationSyncModel currentModel)
    {
        //base.OnRealtimeModelReplaced(previousModel, currentModel);
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.triggerDidChange -= TriggerDidChange;
            
        }

        if (currentModel != null)
        {
            // If this is a model that has no data set on it, populate it with the current mesh renderer color.
            if (currentModel.isFreshModel)
                
                currentModel.trigger = _triggerAnimator.GetFloat("Trigger");


            // Update the mesh render to match the new model
            //UpdateMeshRendererColor();
            UpdateTriggerValue();

            // Register for events so we'll know if the color changes later
            currentModel.triggerDidChange += TriggerDidChange;

        }


    }

    private void TriggerDidChange(TriggerAnimationSyncModel model, float value)
    {
        // Update the mesh renderer
        UpdateTriggerValue();
    }


    private void UpdateTriggerValue()
    {
        // Get the color from the model and set it on the mesh renderer.
        //_meshRenderer.material.color = model.color;
        
        _triggerAnimator.SetFloat("Trigger", model.trigger);

    }

    public void SetTriggerValue(float val)
    {
        model.trigger = val;
    }
}
