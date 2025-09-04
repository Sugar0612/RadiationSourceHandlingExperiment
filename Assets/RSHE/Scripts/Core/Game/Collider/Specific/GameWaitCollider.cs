using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameWaitCollider : NetworkBehaviour
{
    [SyncVar] int _personCount = 0;

    // public List<GameObject> GamePanelList = new List<GameObject>();

    Dictionary<EIdentity, int> _personDic = new Dictionary<EIdentity, int>() { { EIdentity.A1, 0 }, { EIdentity.A2, 0 }, { EIdentity.B1, 0 }, { EIdentity.B2, 0 }, { EIdentity.C1, 0 }, { EIdentity.C2, 0 },{ EIdentity.C3, 0 }, };

    Dictionary<EIdentity, VRNetworkPlayerController> _playerCtrlDic = new Dictionary<EIdentity, VRNetworkPlayerController>() { { EIdentity.A1, null }, { EIdentity.A2, null }, { EIdentity.B1, null }, { EIdentity.B2, null }, { EIdentity.C1, null }, { EIdentity.C2, null }, { EIdentity.C3, null }, };

    private void Awake()
    {
        // SetPanelButtonEnable(false, new List<string>() { "UsrButton" });

        EventManager.OnButtonEnable?.Invoke(false);
    }

    [ServerCallback]
    public void OnTriggerStay(Collider other)
    {
        CheckNumberOfPersonInColliderBox(other);
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        if (ctrl && ctrl.identity != EIdentity.None && _personDic[ctrl.identity] == 1 && _personCount - 1 >= 0)
        {
            _playerCtrlDic[ctrl.identity] = null;
            _personDic[ctrl.identity] = 0;
            _personCount--;

            if (EventManager.OnEventTriggered != null)
                EventManager.OnEventTriggered.Invoke(_personCount);

            if (_personCount != StaticGlobalVar.PersonCount)
                EventManager.OnButtonEnable.Invoke(false);
            // SetPanelButtonEnable(false, new List<string>() { "UsrButton" });
        }
    }

    //public void SetPanelButtonEnable(bool enable, List<string> excludeName)
    //{   
    //    foreach (var panel in GamePanelList)
    //    {
    //        if (panel == null) continue;
    //        Button[] buttons = panel.GetComponentsInChildren<Button>();

    //        foreach (Button button in buttons)
    //        {
    //            if (excludeName.Contains(button.name)) continue;
    //            button.enabled = enable;
    //            button.interactable = enable;
    //        }
    //    }
    //}

    [Server]
    public void CheckNumberOfPersonInColliderBox(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        // counting
        if (ctrl && ctrl.identity != EIdentity.None && _personDic[ctrl.identity] == 0)
        {
            _playerCtrlDic[ctrl.identity] = ctrl;
            _personDic[ctrl.identity] = 1;
            _personCount++;
        }

        // check
        foreach (var pair in _playerCtrlDic)
        {
            if (pair.Value == null && _personDic[pair.Key] == 1)
            {
                _personDic[pair.Key] = 0;
                _personCount--;
            }
        }

        if (_personCount == StaticGlobalVar.PersonCount && StaticGlobalVar.PersonCount != 0)
            EventManager.OnButtonEnable.Invoke(true);
        // SetPanelButtonEnable(true, new List<string>() { "UsrButton" });

        if (EventManager.OnEventTriggered != null)
            EventManager.OnEventTriggered.Invoke(_personCount);
    }

    private void OnDestroy()
    {
        // GamePanelList.Clear();
    }
}
