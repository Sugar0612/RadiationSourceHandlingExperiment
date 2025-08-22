using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneObjectManager : MonoBehaviour
{
    static SceneObjectManager _instance;

    public static SceneObjectManager Get()
    {
        if (_instance == null)
            _instance = FindObjectOfType<SceneObjectManager>(); 

        return _instance;
    }

    #region 场景物体

    /// <summary> 放射源1 </summary>
    public RadiationSource RadioactiveSource_1;

    /// <summary> 放射源2 </summary>
    public RadiationSource RadioactiveSource_2;

    /// <summary> 围栏场景列表 </summary>
    public List<GameObject> OutSideFencesList = new List<GameObject>();

    /// <summary> 内圈警戒带列表 </summary>
    public List<GameObject> InSideFencesList = new List<GameObject>();

    /// <summary> 旗子列表 </summary>
    public List<GameObject> FlagList = new List<GameObject>();

    #endregion

    private void Start()
    {
        Init();
    }

    void Init()
    {
        foreach (var fence in OutSideFencesList)
        {
            CmdSetSceneObjectActive(fence, false);
        }

        foreach (var fence in InSideFencesList)
        {
            CmdSetSceneObjectActive(fence, false);
        }

        foreach (var flag in FlagList)
        {
            CmdSetSceneObjectActive(flag, false);
        }
    }

    public void CmdSetSceneObjectActive(GameObject go, bool active)
    {
        RpcSetSceneObjectActive(go, active);
    }

    void RpcSetSceneObjectActive(GameObject go, bool active)
    {
        // go.gameObject.SetRendererEnable(active);
        go.SetActive<Renderer>(active);
    }

}
 