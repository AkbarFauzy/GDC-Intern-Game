using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageObserver
{
    public void OnNotify(StageEvents stageEvent);
}
