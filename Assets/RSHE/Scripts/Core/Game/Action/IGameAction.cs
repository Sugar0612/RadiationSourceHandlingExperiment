using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameAction
{
    /// <summary> 任务一开始 </summary>
    public void TaskOneStartAction();

    /// <summary> 任务一结束 </summary>
    public void TaskOneEndAction();
}
