using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Mirror;
using UnityEngine;

/// <summary>
/// 推进步骤的Collider:
/// * GamePropCollider √
/// * GamePlayerCollider
public class GamePropCollider : NetworkBehaviour
{
    GameTaskItem _task;

    void Start()
    {
        _task = GetComponentInParent<GameTaskItem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        NetworkPropsCollider propCollider =
            other.gameObject.GetComponentInParent<NetworkPropsCollider>();

        if (propCollider && _task)
        {
            EIdentity identity = propCollider.WhoHolding;

            GameColliderPackage gamePkg = new GameColliderPackage() { TaskItem = _task };
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
