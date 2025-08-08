using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTeleportCollider : NetworkBehaviour
{
    [SyncVar] int _personCount = 0;

    bool _changedScene = false;

    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (StaticGlobalVar.IsServer && ctrl && _changedScene == false)
        {
            _personCount++;

            if (_personCount == StaticGlobalVar.PersonCount)
            {
                _changedScene = true;
                _personCount = 0;
                SceneWindow sceneWin = UIController.Get().GetWindow<SceneWindow>(EWindowType.SceneWindow) as SceneWindow;
                sceneWin.OnClickedModeButton(EGameMode.Teaching);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && StaticGlobalVar.IsServer && _personCount - 1 >= 0)
        {
            _personCount--;
        }
    }
}
