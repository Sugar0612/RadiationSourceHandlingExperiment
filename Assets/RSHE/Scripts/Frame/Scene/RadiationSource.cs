using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadiationSource : NetworkBehaviour
{
    #region D = R * A / d^2

    /// <summary> 剂量常数 </summary>
    public float R = 0.332f;
    
    /// <summary> 放射源活度 </summary>
    public float A = 1110f;

    #endregion

    public bool IsClear { get => isClear(); }

    [SyncVar]
    public int PickupNumber;

    private void Start()
    {

    }

    bool isClear()
    {
        return PickupNumber <= 0;
    }

    [ServerCallback]
    public void SetPickupNumber(int val)
    {
        PickupNumber += val;
    }
}
