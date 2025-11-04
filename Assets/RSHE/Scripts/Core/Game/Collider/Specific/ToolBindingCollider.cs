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

    /// <summary> 抓取点 </summary>
    public Transform GrabTransform;

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
            player.grabHand.GrabObject = gameObject;
            gameObject.transform.parent = player.HeldTrans;
            gameObject.transform.localPosition = new Vector3(GrabTransform.localPosition.x, -GrabTransform.localPosition.y, GrabTransform.localPosition.z);
            gameObject.transform.rotation = GrabTransform.rotation;
            isHeld = true;
        }
    }
}
