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

    /// <summary> 手部模型 </summary>
    [SerializeField] private GameObject _handPose;

    /// <summary> UI控件 </summary>
    [SerializeField] private GameObject _uiCanvas;

    ///// <summary> 生成位置父类 </summary>
    //[SerializeField] private Transform _spawnParent;

    /// <summary>
    /// 预判位置的物体
    /// 当拿起物体后，会出现一个透明的版本在地面，告诉玩家如果点击放置这个东西会放在哪里。
    /// </summary>
    [SerializeField] private GameObject _anticipate;

    [SerializeField] private WarningLinker _warningLinker;

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
        _handPose.SetActive(true);
        _uiCanvas.SetActive(true);
    }

    public override void OnLetGo()
    {
        _pickingUp = false;
        _anticipate.SetActive(false);
        _handPose.SetActive(false);
        _uiCanvas.SetActive(false);
    }

    public override void OnSpawn()
    {
        _anticipate.SetActive(false);
        _handPose.SetActive(false);
        _uiCanvas.SetActive(false);
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
    private void CmdPlace(NetworkConnectionToClient sender = null)
    {
        // 服务器生成
        Vector3 spawnPos = new Vector3(transform.position.x, -0.17f, transform.position.z);
        GameObject newObj = Instantiate(_prefab, spawnPos, Quaternion.identity);
        NetworkServer.Spawn(newObj);

        // 物体放置后，只可以被动链接，不能主动链接。
        RpcCloseActiveLinks(newObj);

        // 生成警戒带
        TargetSpawnSceneLinker(sender, newObj);
    }

    [ClientRpc] private void RpcCloseActiveLinks(GameObject obj)
    {
        var linker = obj.GetComponent<WarningLinker>();
        linker.UnEnable();
    }

    [TargetRpc] private void TargetSpawnSceneLinker(NetworkConnection sender, GameObject obj)
    {
        _warningLinker.SpawnLinker();
    }

    #endregion
}
