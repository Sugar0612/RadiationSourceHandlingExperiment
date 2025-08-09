using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearCollider : MonoBehaviour
{
    GameTaskItem _task;

    public void Start()
    {
        _task = gameObject.GetComponentInParent<GameTaskItem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl =
                GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && _task)
        {
            GameColliderPackage pkg = new GameColliderPackage() { TaskItem = _task };
            _task.OnTask.Invoke(pkg);
        }      
    }

    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl =
                GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && ctrl.WStatus == VRNetworkPlayerController.WearStatus.Wearing)
            ctrl.WStatus = VRNetworkPlayerController.WearStatus.NoWear;
    }
}
