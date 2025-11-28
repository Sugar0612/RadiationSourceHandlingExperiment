using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Collections;
using System.Security.Principal;
using Unity.VisualScripting;

public class ToolRegenerator : NetworkBehaviour
{
    #region 公有成员

    /// <summary> 道具预制件列表</summary>
    public List<GameObject> PropPrefabs = new List<GameObject>();
    
    /// <summary> 场景道具列表 </summary>
    public List<GameObject> PorpScenes = new List<GameObject>();

    /// <summary> 当有新的道具被复制，就要删除场景中已有的相同道具 </summary>
    Dictionary<string, GameObject> _oldSceneProp = new Dictionary<string, GameObject>();

    /// <summary> 道具生成的父节点 </summary>
    public Transform PorpTransform;

    #endregion

    #region 私有成员

    Dictionary<string, GameObject> _propPrefabDic = new Dictionary<string, GameObject>();

    /// <summary> 生成的初始位置 </summary>
    Dictionary<string, Vector3> _spawnPos = new Dictionary<string, Vector3>();

    /// <summary> 生成的初始位置 </summary>
    Dictionary<string, Quaternion> _spawnRot = new Dictionary<string, Quaternion>();

    #endregion

    public override void OnStartServer()
    {
        base.OnStartServer();

        foreach (var prefab in PropPrefabs)
        {
            NetworkPropsCollider propCollider = prefab.GetComponentInChildren<NetworkPropsCollider>();

            if (propCollider == null)
                propCollider = prefab.GetComponentInParent<NetworkPropsCollider>();

            if (propCollider && !_propPrefabDic.ContainsKey(propCollider.PropName))
            {
                _propPrefabDic.Add(propCollider.PropName, prefab);
            }
        }

        foreach (var obj in PorpScenes)
        {
            NetworkPropsCollider propCollider = obj.GetComponentInChildren<NetworkPropsCollider>();

            if (propCollider == null)
                propCollider = obj.GetComponentInParent<NetworkPropsCollider>();

            if (propCollider && !_spawnPos.ContainsKey(propCollider.PropName))
            {
                _spawnPos.Add(propCollider.PropName, obj.transform.position);
            }

            if (propCollider && !_spawnRot.ContainsKey(propCollider.PropName))
            {
                _spawnRot.Add(propCollider.PropName, obj.transform.rotation);
            }
        }
    }

    [Server]
    void RegenerateProp(string propName)
    {
        if (_propPrefabDic.TryGetValue(propName, out GameObject prefab) && _spawnPos.ContainsKey(propName) && _spawnRot.ContainsKey(propName))
        {

            Log.cinput("yellow", "@@ RegenerateProp");
            Vector3 spawnPos = _spawnPos[propName];
            Quaternion spawnRot = _spawnRot[propName];
            GameObject newObj = Instantiate(prefab, spawnPos, spawnRot, PorpTransform);
            NetworkServer.Spawn(newObj);
        }
    }

    [ClientRpc]
    void RpcUnbindObject(NetworkIdentity identity)
    {
        GrabHand grabHand = identity.GetComponentInChildren<GrabHand>();
        if (grabHand.GrabObject)
        {
            grabHand.GrabObject.transform.parent = null;
            grabHand.GrabObject = null;
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        NetworkPropsCollider propCollider = other.GetComponentInChildren<NetworkPropsCollider>();
        if (propCollider == null)
            propCollider = other.GetComponentInParent<NetworkPropsCollider>();

        if (propCollider && !propCollider.isCloned && _propPrefabDic.ContainsKey(propCollider.PropName) && propCollider.PropName == "Transporter") // TODO.
        {
            if (_oldSceneProp.ContainsKey(propCollider.PropName) && _oldSceneProp[propCollider.PropName] != null)
            {
                Utility.DestroyNetworkObject(_oldSceneProp[propCollider.PropName]);
                _oldSceneProp[propCollider.PropName] = other.gameObject;
            }
            else if (_oldSceneProp.ContainsKey(propCollider.PropName) && _oldSceneProp[propCollider.PropName] == null)
            {
                _oldSceneProp[propCollider.PropName] = other.gameObject;
            }
            else
            {
                _oldSceneProp.Add(propCollider.PropName, other.gameObject);
            }   

            // RegenerateProp(propCollider.PropName);
            StartCoroutine(DelayedRegeneration(propCollider.PropName));
            propCollider.isCloned = true;
        }
    }

    [Server]
    IEnumerator DelayedRegeneration(string propName)
    {
        yield return null;
        RegenerateProp(propName);
    }
}
