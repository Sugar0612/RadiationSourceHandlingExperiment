using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Mirror;
using UnityEngine;

/// <summary>
/// 推进步骤的Collider:
/// * GamePropCollider √
/// * GamePlayerCollider
public class GamePlayerCollider : NetworkBehaviour
{
    GameTaskItem _task;

    void Start()
    {
        _task = GetComponentInParent<GameTaskItem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && _task)
        {
            EIdentity identity = ctrl.identity;
            TaskCondition condition = _task.conditions.Find(x => x.identity == identity);

            GameColliderPackage gamePkg = new GameColliderPackage()
            {
                VRPlayerCtrl = ctrl,
                TaskItem = _task,
            };

            _task.OnTask?.Invoke(gamePkg);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        //if (ctrl)
        //{

        //}
    }
}