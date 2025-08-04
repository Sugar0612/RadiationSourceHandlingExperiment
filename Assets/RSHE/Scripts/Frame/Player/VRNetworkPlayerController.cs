using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;

public class VRNetworkPlayerController : NetworkBehaviour
{
    #region 玩家 Transform
    [Header("Location's Transform")]

    [SerializeField]
    [Tooltip("Right Hand Transform")]
    public Transform m_RHand;

    /// <summary> Right Hand Transform. </summary>
    [HideInInspector]
    public Transform rHand { get => m_RHand; set => m_RHand = value; }

    [SerializeField]
    [Tooltip("Left Hand Transform")]
    Transform m_LHand;

    /// <summary> Left Hand Transform. </summary>
    [HideInInspector]
    public Transform lHand { get => m_LHand; set => m_LHand = value; }

    [SerializeField]
    [Tooltip("Head Transform")]
    Transform m_Head;

    /// <summary>Head Transform. </summary>
    [HideInInspector]
    public Transform head { get => m_Head; set => m_Head = value; }

    [Tooltip("Player Collider Transform")]
    public Transform m_PlayerCollider;
    #endregion

    #region 玩家模型
    [Space]
    [Header("Model Prefab")]

    [SerializeField]
    [Tooltip("Player Head Model Component")]
    GameObject m_HeadModel;

    [SerializeField]
    [Tooltip("Player Left Hand Model Component")]
    GameObject m_LHandModel;

    [SerializeField]
    [Tooltip("Player Right Hand Model Component")]
    GameObject m_RHandModel;

    [Tooltip("游戏中帽子")]
    public GameObject hat;

    [Tooltip("游戏中衣服")]
    public GameObject clothes;

    #endregion

    #region 玩家信息 & 组件
    [Space]
    [Header("Other Controller")]

    /// <summary> PlayerRig component on player in scene. </summary>
    private MyVRPlayerRig m_VRPlayerRig;

    /// <summary> Player Name ui component in Scene. </summary>
    public TMP_Text textPlayerName;

    /// <summary> 身份 </summary>
    public EIdentity identity = EIdentity.None;

    /// <summary> Player name variable.</summary>
    [SyncVar(hook = nameof(OnNameChangedHook))]
    string playerName;

    /// <summary> 是否穿戴防护服 </summary>
    [HideInInspector]
    public bool isWearOrNot = false;

    #endregion

    public void Start()
    {
        if (isServer && isLocalPlayer)
            gameObject.SetActive(false);
        else
            StartCoroutine(Config.Get().GetLocalIdentity(arg => identity = arg));

        hat.SetRendererEnable(false);
        clothes.SetRendererEnable(false);

    }

    public void OnNameChangedHook(string _old, string _new)
    {
        if (textPlayerName != null)
        {
            textPlayerName.text = playerName;
        }
    }

    /// <summary> 
    /// To request server revise player name. 
    /// </summary>
    [Command]
    public void CmdSetupName(string _name)
    {
        playerName = _name;
    }

    /// <summary> 
    /// Enable local player. Let the player ignore his own model. 
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        InitObject();

        m_HeadModel.SetRendererEnable(false);
        m_LHandModel.SetRendererEnable(false);
        m_RHandModel.SetRendererEnable(false);
        textPlayerName.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

        StartCoroutine(Config.Get().GetLocalIdentity(arg =>
        {
            CmdSetupName(arg.ToString());
        }));
    }

    /// <summary> 
    /// Init Controller.
    /// </summary>
    public void InitObject()
    {
        if (m_VRPlayerRig == null)
            m_VRPlayerRig = (MyVRPlayerRig)FindObjectOfType(typeof(MyVRPlayerRig));

        if (m_VRPlayerRig != null)
        {
            m_VRPlayerRig.VRPlayerController = this;
        }
    }
}