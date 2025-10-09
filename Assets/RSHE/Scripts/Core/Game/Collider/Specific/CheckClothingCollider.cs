using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class CheckClothingCollider : MonoBehaviour
{
    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController vrCtrl = other.GetComponentInParent<VRNetworkPlayerController>();
        if (vrCtrl && vrCtrl.WStatus != VRNetworkPlayerController.WearStatus.Wore)
        {
            switch (StaticGlobalVar.GameMode)
            {
                case EGameMode.SelfTest:
                    vrCtrl.Prompt(PromptType.WearClothing, 5.0f);
                    break;
                case EGameMode.Assessment:
                    // TODO..
                    break;
                default: break;
            }
        }
    }
}
