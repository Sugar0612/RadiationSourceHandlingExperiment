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
            vrRig.HintPanel.ShowHintPanel("Î´´©´÷·À»¤·þ£¡ÇëÁ¢¼´·µ»Ø´©´÷£¡", 5f);
        }
    }
}
