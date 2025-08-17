using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Collections;

public class ToolRegenerator : NetworkBehaviour
{
    #region 公有成员

    /// <summary> 道具预制件列表</summary>
    public List<GameObject> PropPrefabs = new List<GameObject>();
    
    /// <summary> 场景道具列表 </summary>
    public List<GameObject> PorpScenes = new List<GameObject>();

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
            NetworkPropsCollider propCollider = prefab.GetComponent<NetworkPropsCollider>();
            if (propCollider && !_propPrefabDic.ContainsKey(propCollider.PropName))
            {
                _propPrefabDic.Add(propCollider.PropName, prefab);
            }
        }

        foreach (var obj in PorpScenes)
        {
            NetworkPropsCollider propCollider = obj.GetComponent<NetworkPropsCollider>();
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
            Vector3 spawnPos = _spawnPos[propName];
            Quaternion spawnRot = _spawnRot[propName];
            GameObject newObj = Instantiate(prefab, spawnPos, spawnRot, PorpTransform);
            NetworkServer.Spawn(newObj);   
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider other)
    {
        NetworkPropsCollider propCollider = other.GetComponent<NetworkPropsCollider>();
        if (propCollider && !propCollider.isCloned && _propPrefabDic.ContainsKey(propCollider.PropName))
        {
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