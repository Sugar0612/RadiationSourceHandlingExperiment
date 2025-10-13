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
    public void StartAction_1(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_1(gamePkg);
    }

    /// <summary> task 1 action. </summary>
    public void TaskAction_1(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_1(gamePkg);
    }

    /// <summary> task 1 end. </summary>
    public void EndAction_1(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
         _gameAction.RpcEndAction_1(gamePkg);
    }
   
    /// <summary> task 2 start. </summary>
    public void StartAction_2(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_2(gamePkg);
    }
    
    /// <summary> task 2 action. </summary>
    public void TaskAction_2(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_2(gamePkg);
    }

    /// <summary> task 2 end. </summary>
    public void EndAction_2(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_2(gamePkg);
    }

    /// <summary> task 3 start. </summary>
    public void StartAction_3(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_3(gamePkg);
    }

    /// <summary> task 3 action. </summary>
    public void TaskAction_3(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_3(gamePkg);
    }

    /// <summary> task 3 end. </summary>
    public void EndAction_3(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_3(gamePkg);
    }

    /// <summary> task 4 start. </summary>
    public void StartAction_4(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_4(gamePkg);
    }

    /// <summary> task 4 action. </summary>
    public void TaskAction_4(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_4(gamePkg);
    }

    /// <summary> task 4 end. </summary>
    public void EndAction_4(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_4(gamePkg);
    }

    /// <summary> task 5 start. </summary>
    public void StartAction_5(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_5(gamePkg);
    }

    /// <summary> task 5 action. </summary>
    public void TaskAction_5(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_5(gamePkg);
    }

    /// <summary> task 5 end. </summary>
    public void EndAction_5(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_5(gamePkg);
    }

    /// <summary> task 6 start. </summary>
    public void StartAction_6(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_6(gamePkg);
    }

    /// <summary> task 6 action. </summary>
    public void TaskAction_6(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_6(gamePkg);
    }

    /// <summary> task 6 end. </summary>
    public void EndAction_6(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_6(gamePkg);
    }

    /// <summary> task 7 start. </summary>
    public void StartAction_7(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_7(gamePkg);
    }

    /// <summary> task 7 action. </summary>
    public void TaskAction_7(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_7(gamePkg);
    }

    /// <summary> task 7 end. </summary>
    public void EndAction_7(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_7(gamePkg);
    }

    /// <summary> task 8 start. </summary>
    public void StartAction_8(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_8(gamePkg);
    }

    /// <summary> task 8 action. </summary>
    public void TaskAction_8(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_8(gamePkg);
    }

    /// <summary> task 8 end. </summary>
    public void EndAction_8(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_8(gamePkg);
    }

    /// <summary> task 9 start. </summary>
    public void StartAction_9(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_9(gamePkg);
    }

    /// <summary> task 9 action. </summary>
    public void TaskAction_9(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_9(gamePkg);
    }

    /// <summary> task 9 end. </summary>
    public void EndAction_9(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_9(gamePkg);
    }

    /// <summary> task 1 start. </summary>
    public void StartAction_10(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_10(gamePkg);
    }

    /// <summary> task 1 action. </summary> 
    public void TaskAction_10(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_10(gamePkg);
    }

    /// <summary> task 10 end. </summary>
    public void EndAction_10(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_10(gamePkg);
    }

    /// <summary> task 1 start. </summary>
    public void StartAction_11(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_11(gamePkg);
    }

    /// <summary> task 11 action. </summary>
    public void TaskAction_11(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_11(gamePkg);
    }

    /// <summary> task 11 end. </summary>
    public void EndAction_11(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_11(gamePkg);
    }

    /// <summary> task 12 start. </summary>
    public void StartAction_12(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_12(gamePkg);
    }

    /// <summary> task 12 action. </summary>
    public void TaskAction_12(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_12(gamePkg);
    }

    /// <summary> task 12 end. </summary>
    public void EndAction_12(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_12(gamePkg);
    }

    /// <summary> task 13 start. </summary>
    public void StartAction_13(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartAction_13(gamePkg);
    }

    /// <summary> task 13 action. </summary>
    public void TaskAction_13(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskAction_13(gamePkg);
    }

    /// <summary> task 13 end. </summary>
    public void EndAction_13(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndAction_13(gamePkg);
    }

    /// <summary> task Wait Start. </summary>
    public void StartActionWait(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcStartActionWait(gamePkg);
    }

    /// <summary> task Wait action. </summary>
    public void TaskActionWait(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcTaskActionWait(gamePkg);
    }

    /// <summary> task Wait end. </summary>
    public void EndActionWait(GameColliderPackage gamePkg)
    {
        if (!isServer) return;
        _gameAction.RpcEndActionWait(gamePkg);
    }
}
