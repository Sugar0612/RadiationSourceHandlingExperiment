using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class CheckClothingCollider : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        MyVRPlayerRig vrRig = other.GetComponentInParent<MyVRPlayerRig>();

        if (vrRig && vrRig.VRPlayerController.WStatus != VRNetworkPlayerController.WearStatus.Wore)
        {
            switch (StaticGlobalVar.GameMode)
            {
                case EGameMode.SelfTest:
                    vrRig.HintPanel.ShowHintPanel("Î´´©´÷·À»¤·þ£¡ÇëÁ¢¼´·µ»Ø´©´÷£¡", 5f);
                    break;
                case EGameMode.Assessment:
                    // TODO..
                    break;
                default: break;
            }
        }
    }
}
