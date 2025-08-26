using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAction : NetworkBehaviour
{
    ActionBase _gameAction;

    private void Awake()
    {
        _gameAction = GameModeDispenser.Get().Dispenser(StaticGlobalVar.GameMode);
    }

    /// <summary> task 1 start. </summary>
    public void StartAction_1(GameColliderPackage gamePkg) =>_gameAction.StartAction_1(gamePkg);

    /// <summary> task 1 action. </summary>
    public void TaskAction_1(GameColliderPackage gamePkg) => _gameAction.TaskAction_1(gamePkg);

    /// <summary> task 1 end. </summary>
    public void EndAction_1(GameColliderPackage gamePkg) => _gameAction.EndAction_1(gamePkg);

    /// <summary> task 2 start. </summary>
    public void StartAction_2(GameColliderPackage gamePkg) => _gameAction.StartAction_2(gamePkg);

    /// <summary> task 2 action. </summary>
    public void TaskAction_2(GameColliderPackage gamePkg) => _gameAction.TaskAction_2(gamePkg);

    /// <summary> task 2 end. </summary>
    public void EndAction_2(GameColliderPackage gamePkg) => _gameAction.EndAction_2(gamePkg);

}
