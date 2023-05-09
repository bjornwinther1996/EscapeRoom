using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class LeverIdSync : RealtimeComponent<LeverIdSyncModel>
{
    private LeverIdData _leverIdData;

    private void Awake()
    {
        _leverIdData = GetComponent<LeverIdData>();
    }

    protected override void OnRealtimeModelReplaced(LeverIdSyncModel previousModel, LeverIdSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.leverIdDidChange -= LeverIdDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                currentModel.leverId = _leverIdData._leverId;
            }

            UpdateLeverId();

            currentModel.leverIdDidChange += LeverIdDidChange;
        }
    }

    private void LeverIdDidChange(LeverIdSyncModel model, int value)
    {
        UpdateLeverId();
    }

    private void UpdateLeverId()
    {
        _leverIdData._leverId = model.leverId;
    }

    public int GetLeversPulled()
    {
        return model.leverId;
    }

    public void SetLeverId(int value)
    {
        model.leverId = value;
    }
}
