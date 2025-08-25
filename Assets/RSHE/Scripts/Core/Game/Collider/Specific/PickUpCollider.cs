using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickUpCollider : NetworkBehaviour
{
    /// <summary> 拾取的物体 </summary>
    public GameObject PickUpObject;

    /// <summary> 拾取进度UI </summary>
    public PickUpPanel PickupPanel;

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
        if (collider && collider.IsExist && !_isPicked)
        {
            NetworkPropsCollider porp = other.gameObject.GetComponentInParent<NetworkPropsCollider>();
            if (porp && porp.PropName == CanPickupPorp && !PickupPanel.IsPickingUp)
            {
                PickupPanel.IsPickingUp = true;
                PickupPanel.PickingUp(CmdPickupSuccessed, CmdPickupCancel);
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
                PickupPanel.IsPickingUp = false;
            }
        }
        else if (collider && collider.IsExist && _isPicked)
        {
            PickupPanel.SetActive(false);
        }
    }

    [Command (requiresAuthority = false)]
    void CmdPickupSuccessed()
    {
        if (_radiationSource == null)
            _radiationSource = GetComponentInParent<RadiationSource>();
        _radiationSource.SetPickupNumber(-1);

        RpcPickupSuccessed();
    }

    [ClientRpc]
    void RpcPickupSuccessed()
    {
        _isPicked = true;
        PickupPanel.HintText.text = "已处理";
        PickupPanel.Progress.SetAciveForTheUIControl<Image>(false);

        PickUpObject.SetActive<Renderer>(false);
    }

    [Command(requiresAuthority = false)]
    void CmdPickupCancel()
    {
        RpcPickupCancel();
    }

    [ClientRpc]
    void RpcPickupCancel()
    {
        _isPicked = false;
        PickupPanel.ResetUI();
    }
}
