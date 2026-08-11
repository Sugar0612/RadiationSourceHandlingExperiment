using Mirror;
using UnityEngine;

/// <summary>
/// 道具：警戒桩
/// </summary>
public class WarningPost : NetworkBehaviour
{
    /// <summary> 当前世界位置 </summary>
    private Vector3 _worldPos;

    /// <summary> 生成的预制体 </summary>
    [SerializeField] private GameObject _prefab;

    /// <summary> 生成位置父类 </summary>
    [SerializeField] private Transform _spawnParent;

    #region 放置功能
    /// <summary>
    /// 生成放置物体
    /// </summary>
    public void Place()
    {
        CmdPlace();
    }

    [Command(requiresAuthority = false)]
    private void CmdPlace()
    {
        // 服务器生成
        Vector3 spawnPos = new Vector3(transform.position.x, -0.17f, transform.position.z);
        GameObject newObj = Instantiate(_prefab, spawnPos, Quaternion.identity, _spawnParent);
        NetworkIdentity newObjNetworkIdentity = newObj.GetComponent<NetworkIdentity>();
        NetworkServer.Spawn(newObj);

        // 客户端同步生成物体的父类
        RpcAsyncParent(newObj);
    }

    [ClientRpc] private void RpcAsyncParent(GameObject obj)
    {
        obj.transform.SetParent(_spawnParent);
    }

    #endregion
}
