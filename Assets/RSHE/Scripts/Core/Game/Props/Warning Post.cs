using Mirror;
using UnityEngine;

/// <summary>
/// 道具：警戒桩
/// </summary>
public class WarningPost : PropBase
{
    /// <summary> 当前世界位置 </summary>
    private Vector3 _worldPos;

    /// <summary> 生成的预制体 </summary>
    [SerializeField] private GameObject _prefab;

    ///// <summary> 生成位置父类 </summary>
    //[SerializeField] private Transform _spawnParent;

    /// <summary>
    /// 预判位置的物体
    /// 当拿起物体后，会出现一个透明的版本在地面，告诉玩家如果点击放置这个东西会放在哪里。
    /// </summary>
    [SerializeField] private GameObject _anticipate;

    /// <summary> 是否拿起了 </summary>
    private bool _pickingUp = false;

    #region 系统函数

    private void Start()
    {
        _anticipate.SetActive(false);
        _pickingUp = false;
    }

    private void Update()
    {
        if (_pickingUp)
        {
            _anticipate.transform.position = new Vector3(transform.position.x, -0.17f, transform.position.z);
            _anticipate.transform.rotation = Quaternion.identity;
        }
    }

    #endregion

    #region 事件

    public override void OnPickUp()
    {
        _pickingUp = true;
        _anticipate.SetActive(true);
    }

    public override void OnLetGo()
    {
        _pickingUp = false;
        _anticipate.SetActive(false);
    }

    #endregion

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
        //GameObject newObj = Instantiate(_prefab, spawnPos, Quaternion.identity, _spawnParent);
        GameObject newObj = Instantiate(_prefab, spawnPos, Quaternion.identity);
        NetworkIdentity newObjNetworkIdentity = newObj.GetComponent<NetworkIdentity>();
        NetworkServer.Spawn(newObj);

        // 客户端同步生成物体的父类
        // RpcAsyncParent(newObj);
    }

    //[ClientRpc] private void RpcAsyncParent(GameObject obj)
    //{
    //    obj.transform.SetParent(_spawnParent);
    //}

    #endregion
}
