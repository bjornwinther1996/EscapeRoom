using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Stopwatch : RealtimeComponent<StopwatchModel> {

    public float time {
        get {
            if (model == null) return 0.0f;

            if (model.startTime == 0.0) return 0.0f;

            return (float)(realtime.room.time - model.startTime);
        }
    }

    public void StartStopwatch()
    {
        model.startTime = realtime.room.time;
    }


}
