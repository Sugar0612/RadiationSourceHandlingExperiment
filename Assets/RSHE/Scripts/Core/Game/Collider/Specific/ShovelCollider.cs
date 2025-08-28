using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShovelCollider : NetworkBehaviour
{
    /// <summary> 拾取的物体 </summary>
    public GameObject ShovelObject;

    /// <summary> 拾取进度UI </summary>
    public PickUpPanel ShovelPanel;

    /// <summary> 可以拾取我的道具是什么 </summary>
    public string CanPickupPorp;

    bool _isPicked = false;

    RadiationSource _radiationSource;

    private void Start()
    {
        _radiationSource = GetComponentInParent<RadiationSource>();
    }

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        TransporterCollider collider = gameObject.GetComponentInParent<TransporterCollider>();
        if (collider && collider.IsExist 
            && collider.p_Transporter && collider.p_Transporter.p_JarStatus == Transporter.JarStatus.Open && !_isPicked)
        {
            NetworkPropsCollider porp = other.gameObject.GetComponentInParent<NetworkPropsCollider>();
            if (porp && porp.PropName == CanPickupPorp && !ShovelPanel.IsPickingUp)
            {
                ShovelPanel.IsPickingUp = true;
                ShovelPanel.PickingUp(RpcPickupSuccessed, RpcPickupCancel);
            }
        }
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        TransporterCollider collider = gameObject.GetComponentInParent<TransporterCollider>();
        if (collider && collider.IsExist && !_isPicked)
        {
            NetworkPropsCollider porp = other.gameObject.GetComponentInParent<NetworkPropsCollider>();
            if (porp && porp.PropName == CanPickupPorp)
            {
                ShovelPanel.IsPickingUp = false;
            }
        }
        else if (collider && collider.IsExist && _isPicked)
        {
            ShovelPanel.RpcSetActive(false);
        }
    }

    [ClientRpc]
    void RpcPickupSuccessed()
    {
        _isPicked = true;
        ShovelPanel.HintText.text = "已处理";
        ShovelPanel.Progress.SetAciveForTheUIControl<Image>(false);

        ShovelObject.SetActive<Renderer>(false);
    }

    [ClientRpc]
    void RpcPickupCancel()
    {
        _isPicked = false;
        ShovelPanel.ResetUI();
    }
}
