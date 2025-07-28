using System;
using UnityEngine;

/// <summary>
/// 用来传递游戏重要内容
/// </summary>
[System.Serializable]
public class GameColliderPackage
{
    public VRNetworkPlayerController VRPlayerCtrl;

    public TaskCondition Condition;
}
