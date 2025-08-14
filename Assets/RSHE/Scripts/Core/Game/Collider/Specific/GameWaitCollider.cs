using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameWaitCollider : NetworkBehaviour
{
    [SyncVar] int _personCount = 0;

    public List<GameObject> GamePanelList = new List<GameObject>();

    Dictionary<EIdentity, int> _personDic = new Dictionary<EIdentity, int>() { { EIdentity.A1, 0 }, { EIdentity.A2, 0 }, { EIdentity.B1, 0 }, { EIdentity.B2, 0 }, { EIdentity.C1, 0 }, { EIdentity.C2, 0 },{ EIdentity.C3, 0 }, };

    /// <summary> 这个Event的目的是 配合一些外部操作，可以让不同场景想利用 personCount参数反应到前端页面的开发者提供 </summary>
    public UnityEvent<int> TriggerEvent;

    private void Awake()
    {
        SetPanelButtonEnable(false);
    }

    [ServerCallback]
    public void OnTriggerStay(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && ctrl.identity != EIdentity.None && _personDic[ctrl.identity] == 0)
        {
            _personDic[ctrl.identity] = 1;
            _personCount++;

            if (TriggerEvent != null)
                TriggerEvent.Invoke(_personCount);
            
            if (_personCount == StaticGlobalVar.PersonCount)
                SetPanelButtonEnable(true);
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    VRNetworkPlayerController ctrl =
    //        other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

    //    if (StaticGlobalVar.IsServer && ctrl)
    //    {
    //        _personCount++;

    //        if (_personCount == StaticGlobalVar.PersonCount)
    //        {
    //            SetPanelButtonEnable(true);
    //        }
    //    }
    //}

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        if (ctrl && ctrl.identity != EIdentity.None && _personDic[ctrl.identity] == 1 && _personCount - 1 >= 0)
        {
            _personDic[ctrl.identity] = 0;
            _personCount--;

            if (TriggerEvent != null)
                TriggerEvent.Invoke(_personCount);

            if (_personCount != StaticGlobalVar.PersonCount)
                SetPanelButtonEnable(false);
        }
    }

    public void SetPanelButtonEnable(bool enable)
    {
        foreach (var panel in GamePanelList)
        {
            Button[] buttons = panel.GetComponentsInChildren<Button>();

            foreach (Button button in buttons)
            {
                button.enabled = enable;
            }
        }
    }
}
