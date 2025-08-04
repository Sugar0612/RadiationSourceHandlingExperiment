using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAction : NetworkBehaviour
{
    ActionBase _gameAction;

    private void Awake()
    {
        Log.cinput("yellow", $"@@ StaticGlobalVar.Mode: {StaticGlobalVar.GameMode.ToString()}");
        _gameAction = GameModeDispenser.Get().Dispenser(StaticGlobalVar.GameMode);
    }

    /// <summary> task 1 start. </summary>
    [Command(requiresAuthority = false)] public void CmdStartAction_1(GameColliderPackage gamePkg) =>_gameAction.RpcStartAction_1(gamePkg);

    /// <summary> task 1 action. </summary>
    [Command(requiresAuthority = false)] public void CmdTaskAction_1(GameColliderPackage gamePkg) => _gameAction.RpcTaskAction_1(gamePkg);

    /// <summary> task 1 end. </summary>
    [Command(requiresAuthority = false)] public void CmdEndAction_1(GameColliderPackage gamePkg) => _gameAction.RpcEndAction_1(gamePkg);

    /// <summary> task 2 start. </summary>
    [Command(requiresAuthority = false)] public void CmdStartAction_2(GameColliderPackage gamePkg) => _gameAction.RpcStartAction_2(gamePkg);

    /// <summary> task 2 action. </summary>
    [Command(requiresAuthority = false)] public void CmdTaskAction_2(GameColliderPackage gamePkg) => _gameAction.RpcTaskAction_2(gamePkg);

    /// <summary> task 2 end. </summary>
    [Command(requiresAuthority = false)] public void CmdEndAction_2(GameColliderPackage gamePkg) => _gameAction.RpcEndAction_2(gamePkg);

}
