using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WarningLinker : NetworkBehaviour
{
    public class linkerPkg
    {
        public WarningLinker linker;
        public Linker linkerObj;
    }
    private List<linkerPkg> _linkers = new List<linkerPkg>();

    public Transform linkerPointer;

    public bool canlink = true;

    /// <summary> 是否可以主动链接 </summary>
    public bool activeLinking = false;

    [SerializeField] private GameObject _virtualLinkPrefab;

    [SerializeField] private GameObject _realLinkPrefab;

    /// <summary>
    /// 检测
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerStay(Collider other)
    {
        var linker = other.GetComponent<WarningLinker>();
        if (linker != null && linker.canlink && activeLinking)
        {
            linker.canlink = false;

            var linkerObj = GameObject.Instantiate(_virtualLinkPrefab).GetComponent<Linker>();
            linkerObj.Init(linkerPointer, linker.linkerPointer);

            linkerPkg linkPkg = new linkerPkg();
            linkPkg.linker = linker;
            linkPkg.linkerObj = linkerObj;

            _linkers.Add(linkPkg);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var linker = other.GetComponent<WarningLinker>();
        if (linker != null && activeLinking)
        {
            var pkg = _linkers.Find(_ => _.linker = linker);
            if (pkg != null)
            {
                linker.canlink = true;
                pkg.linkerObj.gameObject.SetActive(false);
                _linkers.Remove(pkg);
            }
        }
    }

    #region Spawn

    public void UnEnable()
    {
        activeLinking = false;

        foreach (var p in _linkers)
        {
            p.linkerObj.gameObject.SetActive(false);
            Destroy(p.linkerObj.gameObject);
        }

        _linkers.Clear();

        var sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = false;
        sphereCollider.enabled = false;
    }

    /// <summary>
    /// 在场景中生成固定警戒带，外部接口
    /// </summary>
    public void SpawnLinker()
    {
        // 对可连接的【警戒柱】依次生成固定警戒带
        foreach (var p in _linkers)
        {
            var point = p.linker.linkerPointer;
            CmdSpawnRealLinker(linkerPointer.position, point.position);
        }
    }

    /// <summary>
    /// 在场景中生成固定警戒带
    /// </summary>
    /// <param name="targetA"></param>
    /// <param name="targetB"></param>
    [Command(requiresAuthority = false)] private void CmdSpawnRealLinker(Vector3 a, Vector3 b)
    {
        // 生成场景真实警戒带
        var linkerObj = GameObject.Instantiate(_realLinkPrefab);

        Vector3 direction = b - a;
        float distance = direction.magnitude;

        // 中点
        linkerObj.transform.position = (a + b) * 0.5f;

        // 让 X 轴指向 B
        linkerObj.transform.rotation = Quaternion.FromToRotation(
            Vector3.right,
            direction
        );

        // 修改 X 轴长度
        Vector3 scale = linkerObj.transform.localScale;
        scale.x = distance / 1.0f;
        linkerObj.transform.localScale = scale;

        NetworkServer.Spawn(linkerObj);

        RpcSetMaterial(linkerObj, scale.x);
    }

    [ClientRpc] private void RpcSetMaterial(GameObject obj, float currScaleX)
    {
        // 修改ShaderGraph
        var r = obj.GetComponent<Renderer>();
        var mat = r.material;
        mat.SetFloat("__2", currScaleX * 10.0f);
        mat.SetFloat("__3", 0.1f / (currScaleX / 1.0f));
    }

    #endregion
}
