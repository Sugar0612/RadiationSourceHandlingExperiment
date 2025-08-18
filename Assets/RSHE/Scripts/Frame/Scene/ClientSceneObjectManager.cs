using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneObjectManager : NetworkBehaviour
{
    [ClientRpc]
    void RpcSetSceneObjectActive(GameObject go, bool active)
    {
        // go.gameObject.SetRendererEnable(active);
    }
}
