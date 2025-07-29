using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAction : MonoBehaviour
{
    IGameAction _gameAction;

    private void Awake()
    {
        Log.cinput("yellow", $"@@ StaticGlobalVar.Mode: {StaticGlobalVar.GameMode.ToString()}");
        _gameAction = GameModeDispenser.Get().Dispenser(StaticGlobalVar.GameMode);
    }

    /// <summary> task 1 start. </summary>
    public void TaskOneStartAction(GameColliderPackage gamePkg = null) => _gameAction.TaskOneStartAction(gamePkg);

    /// <summary> task 1 action. </summary>
    public void TaskOneAction(GameColliderPackage gamePkg = null) => _gameAction.TaskOneAction(gamePkg);

    /// <summary> task 1 end. </summary>
    public void TaskOneEndAction(GameColliderPackage gamePkg = null) => _gameAction.TaskOneEndAction(gamePkg);
    
}
