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
    public bool isHeld = false;

    /// <summary> 拿取道具时Transform </summary>
    public Transform HeldShapeTransform;

    /// <summary> Root node Transform </summary>
    public Transform RootTransform;

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        NetworkIdentity identity = other.GetComponentInParent<NetworkIdentity>();
        if (identity)
        {
            RpcSetHeld(identity);
        }
    }

    [ClientRpc]
    void RpcSetHeld(NetworkIdentity identity)
    {
        VRNetworkPlayerController player = identity.GetComponent<VRNetworkPlayerController>();
        if (player && player.grabHand.GrabObject == null && !isHeld)
        {
            gameObject.transform.parent = player.HeldTrans;
            player.grabHand.GrabObject = gameObject;

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            RootTransform.localPosition = HeldShapeTransform.localPosition;
            RootTransform.rotation = HeldShapeTransform.rotation;
            isHeld = true;
        }
    }
}
