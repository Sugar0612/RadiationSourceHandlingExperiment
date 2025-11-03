using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ToolBindingCollider : NetworkBehaviour
{
    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        Log.cinput("red", "@@@@@@@@ ToolBindingCollider");
        VRNetworkPlayerController player = other.GetComponentInParent<VRNetworkPlayerController>();
        NetworkIdentity identity = other.GetComponentInParent<NetworkIdentity>();
        if (player)
        {
            RpcSetHeld(identity);
        }

    }

    [ClientRpc]
    void RpcSetHeld(NetworkIdentity identity)
    {
        VRNetworkPlayerController player = identity.GetComponent<VRNetworkPlayerController>();
        gameObject.transform.parent = player.HeldTrans;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
    }
}
