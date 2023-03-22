using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ElevatorSync : RealtimeComponent<ElevatorSyncModel>
{

    private ElevatorData _elevator;

    private void Awake()
    {
        _elevator = GetComponent<ElevatorData>();
    }

    protected override void OnRealtimeModelReplaced(ElevatorSyncModel previousModel, ElevatorSyncModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.goUpDidChange -= GoUpDidChange;
            previousModel.goDownDidChange -= GoDownDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                currentModel.goUp = _elevator._goUp;
                currentModel.goDown = _elevator._goDown;
            }

            UpdateGoUp();
            UpdateGoDown();

            currentModel.goUpDidChange += GoUpDidChange;
            currentModel.goDownDidChange += GoDownDidChange;
        }
    }

    private void GoUpDidChange(ElevatorSyncModel model, bool value)
    {
        UpdateGoUp();
    }

    private void GoDownDidChange(ElevatorSyncModel model, bool value)
    {
        UpdateGoDown();
    }

    private void UpdateGoUp()
    {
        _elevator._goUp = model.goUp;
    }

    private void UpdateGoDown()
    {
        _elevator._goDown = model.goDown;
    }

    public bool GetGoUp()
    {
        return model.goUp;
    }

    public void SetGoUp(bool value)
    {
        model.goUp = value;
    }

    public bool GetGoDown()
    {
        return model.goDown;
    }

    public void SetGoDown(bool value)
    {
        model.goDown = value;
    }

}
