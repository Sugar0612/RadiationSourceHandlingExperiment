using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneObjectManager : NetworkBehaviour
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
    public Transform RadioactiveSource_1;

    /// <summary> 放射源2 </summary>
    public Transform RadioactiveSource_2;

    /// <summary> 围栏场景列表 </summary>
    public List<GameObject> FencesList = new List<GameObject>();

    /// <summary> 旗子列表 </summary>
    public List<GameObject> FlagList = new List<GameObject>();

    #endregion

    private void Start()
    {
        Init();
    }

    void Init()
    {
        foreach (var fence in FencesList)
        {
            CmdSetSceneObjectActive(fence, false);
        }

        foreach (var flag in FlagList)
        {
            CmdSetSceneObjectActive(flag, false);
        }
    }

    
}
