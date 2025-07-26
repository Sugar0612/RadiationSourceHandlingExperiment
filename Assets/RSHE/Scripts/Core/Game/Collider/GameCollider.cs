using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class GameCollider : NetworkBehaviour
{
    GameTaskItem task;

    void Start()
    {
        task = GetComponentInParent<GameTaskItem>();    
    }

    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl)
        {
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        if (ctrl)
        {

        }
    }
}
