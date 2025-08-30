using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadiationSource : NetworkBehaviour
{
    #region D = R * A / d^2

    /// <summary> 剂量常数 </summary>
    public float R = 0.332f;
    
    /// <summary> 放射源活度 </summary>
    public float A = 1110f;

    #endregion

    public string RName;

    public List<PickUpCollider> PickUpList = new List<PickUpCollider>();

    public List<ShovelCollider> ShovelList = new List<ShovelCollider>();

    public bool IsPickUpClear { get => isPickUpClear(); }

    [SyncVar]
    public int PickupNumber;

    [SyncVar]
    public int ShovelNumber;

    private void Start()
    {

    }

    bool isShovelClear()
    {
        return ShovelNumber <= 0;
    }

    bool isPickUpClear()
    {
        return PickupNumber <= 0;
    }

    [ServerCallback]
    public void SetShovelNumber(int val)
    {
        ShovelNumber += val;
    }

    [ServerCallback]
    public void SetPickupNumber(int val)
    {
        PickupNumber += val;
    }

    public IEnumerator PickupAllObject(float waitSec)
    {
        yield return new WaitForSeconds(waitSec);

        foreach (PickUpCollider pickup in PickUpList)
        {
            pickup.PickUpObject.SetActive<Renderer>(false);
            pickup.PickupPanel.Progress.SetAciveForTheUIControl<Image>(false);
            pickup.PickupPanel.HintText.SetAciveForTheUIControl<TextMeshProUGUI>(false);
        }
    }

    public IEnumerator ShovelAllObject(float waitSec)
    {
        yield return new WaitForSeconds(waitSec);

        foreach (ShovelCollider shovel in ShovelList)
        {
            shovel.ShovelObject.SetActive<Renderer>(false);
            shovel.ShovelPanel.Progress.SetAciveForTheUIControl<Image>(false);
            shovel.ShovelPanel.HintText.SetAciveForTheUIControl<TextMeshProUGUI>(false);
        }
    }
}
