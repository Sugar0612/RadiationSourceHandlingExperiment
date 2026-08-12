using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolBindingCollider : NetworkBehaviour
{
    public GameObject propPrefab;

    public Transform parentTransform;

    [SyncVar(hook = nameof(OnPropObjectNetIdChanged))]
    private uint propObjectNetId;

    public PropBase propObject;

    public Button operatorButton;

    Vector3 spawnPos;

    Quaternion spawnRot;

    [SyncVar(hook = nameof(OnTakeIdentityChanged))]
    public EIdentity takeIdentity = EIdentity.None;

    #region 系统函数

    private void Start()
    {
        operatorButton.onClick.AddListener(ClickedOperatorButton);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        propObject.transform.GetPositionAndRotation(out spawnPos, out spawnRot);
        //spawnPos = propObject.transform.localPosition;
        //spawnRot = propObject.transform.localRotation;
    }

    #endregion

    void ClickedOperatorButton()
    {
        EIdentity localIdentity = GetLocalPlayerIdentity();

        if (takeIdentity == EIdentity.None)
        {
            propObject.OnPickUp();
            CmdSetTakeIdentity(localIdentity);
        }
        else if (takeIdentity == localIdentity)
        {
            propObject.OnLetGo();
            CmdSetTakeIdentity(EIdentity.None);
        }
    }

    [Command(requiresAuthority = false)]
    void CmdSetTakeIdentity(EIdentity targetIdentity)
    {
        NetworkPropsCollider propInfo = propObject.GetComponent<NetworkPropsCollider>();
        if (propInfo && targetIdentity == EIdentity.None)
        {
            Utility.DestroyNetworkObject(propObject.gameObject);

            GameObject newObj = Instantiate(propPrefab, spawnPos, spawnRot, parentTransform);
            NetworkIdentity newObjNetworkIdentity = newObj.GetComponent<NetworkIdentity>();

            NetworkServer.Spawn(newObj);
            propObjectNetId = newObjNetworkIdentity.netId;
            propObject = newObj.GetComponent<PropBase>();
        }
        else if (targetIdentity != EIdentity.None)
        {
            VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(targetIdentity);
            if (player.grabHand.GrabObject != null)
                return;
        }
        takeIdentity = targetIdentity;
    }

    void OnTakeIdentityChanged(EIdentity oldIdentity, EIdentity newIdentity)
    {
        
        NetworkPropsCollider propInfo = propObject.GetComponent<NetworkPropsCollider>();
        if (newIdentity == EIdentity.None)
        {
            VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(oldIdentity);
            player.ClearGrabObject();
            operatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "拾取";
        }
        else
        {
            operatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "放回";
            VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(newIdentity);

            if (propInfo && player && player.grabHand.GrabObject == null)
            {
                propObject.transform.parent = player.HeldTrans;
                player.grabHand.GrabObject = propObject.gameObject;

                propObject.transform.localPosition = Vector3.zero;
                propObject.transform.localRotation = Quaternion.identity;
                propInfo.RootTransform.localPosition = propInfo.HeldShapeTransform.localPosition;
                propInfo.RootTransform.rotation = propInfo.HeldShapeTransform.rotation;
            }
        }
    }

    private void OnPropObjectNetIdChanged(uint oldNetId, uint newNetId)
    {
        if (newNetId == 0)
        {
            propObject = null;
            return;
        }

        // 通过 netId 在客户端已生成的对象中查找
        if (NetworkClient.spawned.TryGetValue(newNetId, out NetworkIdentity networkIdentity))
        {
            propObject = networkIdentity.gameObject.GetComponent<PropBase>();
        }
        else
        {
            Debug.LogWarning($"Client: NetworkIdentity with netId {newNetId} not found in spawned list.");
        }
    }


    EIdentity GetLocalPlayerIdentity()
    {
        VRNetworkPlayerController[] players = FindObjectsOfType<VRNetworkPlayerController>();
        foreach (VRNetworkPlayerController player in players)
        {
            if (player.isLocalPlayer)
            {
                return player.identity;
            }
        }
        return EIdentity.None;
    }
}
