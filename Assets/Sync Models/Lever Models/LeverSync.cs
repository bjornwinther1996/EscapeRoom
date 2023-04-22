using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class LeverSync : RealtimeComponent<LeverSyncModel>
{
    private LeverData _leverData;

    private void Awake()
    {
        _leverData = GetComponent<LeverData>();
    }

    protected override void OnRealtimeModelReplaced(LeverSyncModel previousModel, LeverSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.leversPulledDidChange -= LeversPulledDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                currentModel.leversPulled = _leverData._leversPulled;
            }

            UpdateLeversPulled();

            currentModel.leversPulledDidChange += LeversPulledDidChange;
        }
    }

    private void LeversPulledDidChange(LeverSyncModel model, int value)
    {
        UpdateLeversPulled();
    }

    private void UpdateLeversPulled()
    {
        _leverData._leversPulled = model.leversPulled;
    }

    public int GetLeversPulled()
    {
        return model.leversPulled;
    }

    public void SetLeversPulled(int value)
    {
        model.leversPulled = value;
    }
}
