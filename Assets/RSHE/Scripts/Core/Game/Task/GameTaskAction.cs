using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTaskAction : MonoBehaviour
{
    IGameAction _gameAction;

    private void Awake()
    {
        Log.cinput("yellow", $"@@ StaticGlobalVar.Mode: {StaticGlobalVar.Mode.ToString()}");
        _gameAction = GameModeDispenser.Get().Dispenser(StaticGlobalVar.Mode);

        if (_gameAction != null) 
            Log.cinput("yellow", $"@@ _gameAction != null");
        else 
            Log.cinput("yellow", $"@@ _gameAction == null");
    }

    /// <summary>
    /// 任务一开始阶段处理
    /// </summary>
    public void TaskOneStartAction() 
    {
        _gameAction.TaskOneStartAction();
    }

    /// <summary>
    /// 任务一结束阶段处理
    /// </summary>
    public void TaskOneEndAction()
    {
        _gameAction.TaskOneEndAction();
    }
}
