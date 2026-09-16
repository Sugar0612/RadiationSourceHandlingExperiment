using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.PXR;
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
        // 道具可能已被销毁(如上一次放回后引用尚未刷新)
        if (propObject == null) return;

        EIdentity localIdentity = GetLocalPlayerIdentity();
        MyVRPlayerRig localRig = FindObjectOfType<MyVRPlayerRig>();
        if (localRig == null) return;

        if (takeIdentity == EIdentity.None)
        {
            localRig.heldRenderer.material.SetColor("_InnerColor", new Color(0.37f, 0.4f, 0.5f, 0.01f));
            localRig.heldRenderer.material.SetColor("_OutColor", new Color(0.76f, 0.81f, 0.94f, 0.01f));
            propObject.OnPickUp();
            CmdSetTakeIdentity(localIdentity);
        }
        else if (takeIdentity == localIdentity)
        {
            localRig.heldRenderer.material.SetColor("_InnerColor", new Color(0.37f, 0.4f, 0.5f, 0.65f));
            localRig.heldRenderer.material.SetColor("_OutColor", new Color(0.76f, 0.81f, 0.94f, 0.65f));
            propObject.OnLetGo();
            CmdSetTakeIdentity(EIdentity.None);
        }
    }

    [Command(requiresAuthority = false)]
    void CmdSetTakeIdentity(EIdentity targetIdentity, NetworkConnectionToClient sender = null)
    {
        if (targetIdentity == EIdentity.None)
        {
            // 放回操作:道具必须是已被拿取状态,防止连点重复销毁/重建
            if (takeIdentity == EIdentity.None) return;

            // 只有当前持有人可以放回
            if (sender != null && sender.identity != null)
            {
                VRNetworkPlayerController senderPlayer = sender.identity.GetComponent<VRNetworkPlayerController>();
                if (senderPlayer == null || senderPlayer.identity != takeIdentity) return;
            }

            NetworkPropsCollider propInfo = propObject != null ? propObject.GetComponent<NetworkPropsCollider>() : null;
            if (propInfo == null)
            {
                Log.cinput("red", $"CmdSetTakeIdentity: {propObject} missing NetworkPropsCollider, put back aborted.");
                return;
            }

            Utility.DestroyNetworkObject(propObject.gameObject);

            // Spawn
            GameObject newObj = Instantiate(propPrefab, spawnPos, spawnRot, parentTransform);
            NetworkIdentity newObjNetworkIdentity = newObj.GetComponent<NetworkIdentity>();
            NetworkServer.Spawn(newObj);

            // OnSpawn
            RpcPropSpawn(newObj);

            propObjectNetId = newObjNetworkIdentity.netId;
            propObject = newObj.GetComponent<PropBase>();
            takeIdentity = targetIdentity;
        }
        else
        {
            VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(targetIdentity);
            if (player == null || player.grabHand == null) return;
            if (player.grabHand.GrabObject != null) return;

            takeIdentity = targetIdentity;
        }
    }

    [ClientRpc] private void RpcPropSpawn(GameObject propObj)
    {
        if (propObj == null) return;

        var p = propObj.GetComponent<PropBase>();
        if (p == null) return;

        p.OnSpawn();
    }

    void OnTakeIdentityChanged(EIdentity oldIdentity, EIdentity newIdentity)
    {
        // 重连初期或道具销毁瞬间,propObject 可能尚未刷新
        if (propObject == null) return;

        NetworkPropsCollider propInfo = propObject.GetComponent<NetworkPropsCollider>();
        TextMeshProUGUI buttonText = operatorButton != null ? operatorButton.GetComponentInChildren<TextMeshProUGUI>() : null;

        if (newIdentity == EIdentity.None)
        {
            VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(oldIdentity);
            if (player != null && player.grabHand != null)
                player.ClearGrabObject();

            if (buttonText != null)
                buttonText.text = "拾取";
        }
        else
        {
            if (buttonText != null)
                buttonText.text = "放回";

            VRNetworkPlayerController player = PlayerManager.Get().GetPlayer(newIdentity);

            if (propInfo && player && player.grabHand != null && player.grabHand.GrabObject == null && player.HeldTrans != null)
            {
                propObject.transform.parent = player.HeldTrans;
                player.grabHand.GrabObject = propObject.gameObject;

                if (propInfo.RootTransform != null && propInfo.HeldShapeTransform != null)
                {
                    propObject.transform.localPosition = Vector3.zero;
                    propObject.transform.localRotation = Quaternion.identity;
                    propInfo.RootTransform.localPosition = propInfo.HeldShapeTransform.localPosition;
                    propInfo.RootTransform.rotation = propInfo.HeldShapeTransform.rotation;
                }
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
