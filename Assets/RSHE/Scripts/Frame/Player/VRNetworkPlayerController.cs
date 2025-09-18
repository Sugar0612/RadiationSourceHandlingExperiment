using Mirror;
using System.Collections;
using TMPro;
using Unity.XR.PXR;
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

    [Tooltip("游戏中左手套")]
    public GameObject LeftGlove;

    [Tooltip("游戏中右手套")]
    public GameObject RightGlove;

    [Tooltip("游戏中眼镜")]
    public GameObject Spectacles;
   
    [Tooltip("游戏中围脖")]
    public GameObject Collar;

    [Tooltip("游戏中衣服")]
    public GameObject Clothes;

    [Tooltip("游戏中衣服")]
    public GameObject Hat;

    #endregion

    #region 玩家信息 & 组件
    [Space]
    [Header("Other Controller")]

    /// <summary> PlayerRig component on player in scene. </summary>
    private MyVRPlayerRig m_VRPlayerRig;

    /// <summary> Player Name ui component in Scene. </summary>
    public TMP_Text textPlayerName;

    /// <summary> 身份 </summary>
    [SyncVar(hook = nameof(OnIdentityChanged))]
    public EIdentity identity = EIdentity.None;

    /// <summary> Player name variable.</summary>
    [SyncVar(hook = nameof(OnNameChangedHook))]
    public string playerName;

    /// <summary> 是否穿戴防护服 </summary>
    public WearStatus WStatus = WearStatus.NoWear;

    private ulong _lhandState = 1;

    private ulong _rhandState = 1;

    #endregion

    public void Start()
    {
        LeftGlove.SetRendererEnable(false);
        RightGlove.SetRendererEnable(false);
        Clothes.SetRendererEnable(false);
        Spectacles.SetRendererEnable(false);
        Collar.SetRendererEnable(false);
        Hat.SetRendererEnable(false);
    }

    public void Update()
    {
        HandAimState rightState = new HandAimState();
        PXR_HandTracking.GetAimState(HandType.HandRight, ref rightState);
        if ((ulong)rightState.aimStatus != _rhandState)
        {
            _rhandState = (ulong)rightState.aimStatus;
            CmdSetRightHandEnable(!(rightState.aimStatus == 0));
        }

        HandAimState leftState = new HandAimState();
        PXR_HandTracking.GetAimState(HandType.HandLeft, ref leftState);
        if ((ulong)leftState.aimStatus != _lhandState)
        {
            _lhandState = (ulong)leftState.aimStatus;
            CmdSetLeftHandEnable(!(leftState.aimStatus == 0));
        }
        

    }

    [Command(requiresAuthority = false)]
    public void CmdSetRightHandEnable(bool enable)
    {
        RpcSetRightHandEnable(enable);
    }

    [ClientRpc]
    public void RpcSetRightHandEnable(bool enable)
    {
        if (!isLocalPlayer)
        {
            m_RHandModel.SetRendererEnable(enable);
            if (enable)
            {
                RightGlove.SetRendererEnable(GameSteps.Get().CheckTaskGoRun(TaskName.T1));
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSetLeftHandEnable(bool enable)
    {
        RpcSetLeftHandEnable(enable);
    }

    [ClientRpc]
    public void RpcSetLeftHandEnable(bool enable)
    {
        if (!isLocalPlayer)
        {
            m_LHandModel.SetRendererEnable(enable);
            if (enable)
            {
                LeftGlove.SetRendererEnable(GameSteps.Get().CheckTaskGoRun(TaskName.T1));
            }
        }
    }


    public void OnNameChangedHook(string _old, string _new)
    {
        if (textPlayerName != null)
        {
            textPlayerName.text = playerName;
        }
    }

    private void OnIdentityChanged(EIdentity oldValue, EIdentity newValue)
    {
        // 更新游戏逻辑，比如更新UI、改变外观等
        Debug.Log($"Player identity changed from {oldValue.ToString()} to {newValue.ToString()}");
    }

    [Command(requiresAuthority = false)]
    private void CmdSetIdentity(EIdentity _identity)
    {
        identity = _identity;
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
        if (isServer && isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
        else if (!isServer &&　isLocalPlayer)
        {
            StartCoroutine(Config.Get().GetLocalIdentity(arg =>
            {
                CmdSetIdentity(arg);
            }));

            StartCoroutine(Config.Get().GetLocalIdentity(arg =>
            {
                CmdSetupName(arg.ToString());
            }));
        }

        InitObject();

        m_HeadModel.SetRendererEnable(false);
        m_LHandModel.SetRendererEnable(false);
        m_RHandModel.SetRendererEnable(false);
        textPlayerName.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    public override void OnStopServer()
    {
        //Log.cinput("yellow", $"Client Disconnected! Device ID: {identity.ToString()}\n");

        UserWindow userWin = UIController.Get().GetWindow<UserWindow>(EWindowType.UserWindow) as UserWindow;
        userWin.SetItemState(identity, EUserState.Offline);
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

    [ClientRpc] public void RpcSetWStatus(WearStatus status) => WStatus = status;

    public enum WearStatus
    {
        NoWear,
        Wearing,
        Wore,
        None
    }
}
