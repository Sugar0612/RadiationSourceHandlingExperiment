using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneObjectManager : NetworkBehaviour
{
    [Command(requiresAuthority = false)] 
    public void CmdSetSceneObjectActive(GameObject go, bool active)
    {
        RpcSetSceneObjectActive(go, active);
    }
}
