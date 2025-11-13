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
                    vrCtrl.TargetPrompt(vrCtrl.connectionToClient, PromptType.WearClothing);
                    break;
                case EGameMode.Assessment:
                    Scorer.Get().Deduction(TaskName.T1, vrCtrl.identity, GameSteps.Get().currTask.fraction, PromptType.WearClothing);
                    break;
                default: break;
            }
        }
    }
}
