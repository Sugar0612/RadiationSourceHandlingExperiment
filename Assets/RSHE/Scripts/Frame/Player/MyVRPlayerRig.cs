using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using RootMotion.FinalIK;

public class MyVRPlayerRig : MonoBehaviour
{
    #region Model Transform

    [Header("Model Transform")]

    [SerializeField]
    [Tooltip("model's lefthand tranform")]
    Transform m_LHand;

    /// <summary> model's lefthand tranform </summary>
    public Transform lHand { get => m_LHand; set => m_LHand = value; }

    [SerializeField]
    [Tooltip("model's righthand tranform")]
    Transform m_RHand;

    /// <summary> model's righthand tranform </summary>
    public Transform rHand { get => m_RHand; set => m_RHand = value; }

    [SerializeField]
    [Tooltip("model's headtranform")]
    Transform m_Head;

    /// <summary> model's headtranform </summary>
    public Transform head { get => m_Head; set => m_Head = value; }

    #endregion

    public SkinnedMeshRenderer heldRenderer;

    #region Player Controller
    [Space]
    [Header("Controller")]

    [SerializeField]
    [Tooltip("Prefab model manager. Used to synchronize the transform of different parts of the model with the corresponding parts of the VR origin of the real scene.")]
    VRNetworkPlayerController _vRPlayerController;

    /// <summary> Prefab model manager, used to synchronize the transform of different parts of the model with the corresponding parts of the VR origin of the real scene. </summary>
    public VRNetworkPlayerController VRPlayerController { get => _vRPlayerController; set => _vRPlayerController = value; }

    VRPlayerController m_VRPlayerCtrl;

    public VRPlayerController vrPlayerCtrl { get => m_VRPlayerCtrl; set => m_VRPlayerCtrl = value; }

    /// <summary> 渐变效果 </summary>
    private VRScreenFade vrScreenFade;

    /// <summary> 用户消息窗口 </summary>
    public PlayerHintPanel HintPanel;

    #endregion

    #region VR IK
    [Space]
    [Header("VR IK")]

    Transform ikHeadTarget;
    
    Transform ikRightHandTarget;
    
    Transform ikLeftHandTarget;

    #endregion

    public void Awake()
    {
        if (vrScreenFade == null)
            vrScreenFade = (VRScreenFade)FindObjectOfType(typeof(VRScreenFade));
    }

    public void Start()
    {
        vrScreenFade.SetAlphaVar(1.0f, 0.0f);
        vrScreenFade.enabled = true;

        if (StaticGlobalVar.IsHost)
        {
            gameObject.SetActive(false);
        }

        //if (Config.Get().PicoDevice)
        //    StartCoroutine(StaticGlobalVar.NetworkDiscovery.IEStartDiscovery());
    }

    void FixedUpdate()
    {
        VRTemplatePlayerModelSync();
        VRPlayerModleSync();
    }

    private void VRTemplatePlayerModelSync()
    {
        if (VRPlayerController)
        {
            VRPlayerController.head.position = head.transform.position;
            VRPlayerController.head.rotation = head.transform.rotation;

            VRPlayerController.lHand.position = lHand.transform.position;
            VRPlayerController.lHand.rotation = lHand.transform.rotation;

            VRPlayerController.rHand.position = rHand.transform.position;
            VRPlayerController.rHand.rotation = rHand.transform.rotation;
        }
    }

    void VRPlayerModleSync()
    {
        if (vrPlayerCtrl && vrPlayerCtrl.ik && !NetworkServer.active)
        {
            ikHeadTarget = GameObject.Find("IKHeadTarget")?.transform;
            ikLeftHandTarget = GameObject.Find("IKLeftHandTarget")?.transform;
            ikRightHandTarget = GameObject.Find("IKRightHandTarget")?.transform;

            vrPlayerCtrl.ik.solver.spine.headTarget = ikHeadTarget;
            vrPlayerCtrl.ik.solver.leftArm.target = ikLeftHandTarget;
            vrPlayerCtrl.ik.solver.rightArm.target = ikRightHandTarget;

            if (CheckIKSolverArm(vrPlayerCtrl.ik.solver.leftArm.target)) { SetIKArmPosAndRotWeight(vrPlayerCtrl.ik.solver.leftArm, 1.0f, 0.8f); }
            if (CheckIKSolverArm(vrPlayerCtrl.ik.solver.rightArm.target)) { SetIKArmPosAndRotWeight(vrPlayerCtrl.ik.solver.rightArm, 1.0f, 0.8f); }
            if (!CheckIKSolverArm(vrPlayerCtrl.ik.solver.leftArm.target)) { SetIKArmPosAndRotWeight(vrPlayerCtrl.ik.solver.leftArm); }
            if (!CheckIKSolverArm(vrPlayerCtrl.ik.solver.rightArm.target)) { SetIKArmPosAndRotWeight(vrPlayerCtrl.ik.solver.rightArm); }
        }
    }

    /// <summary> 检查 vrik.solver.arm 是否符合条件 </summary>
    bool CheckIKSolverArm(Transform armTrans)
    {
        return armTrans != null && armTrans.position.x != 0.0f && armTrans.position.z != 0.0f;
    }

    /// <summary> 设置 vrik.solver.arm 的 position和rotation的 weight. </summary>
    void SetIKArmPosAndRotWeight(IKSolverVR.Arm arm, float posWeight = 0.0f, float rotWeight = 0.0f)
    {
        arm.positionWeight = posWeight;
        arm.rotationWeight = rotWeight;
    }
}
