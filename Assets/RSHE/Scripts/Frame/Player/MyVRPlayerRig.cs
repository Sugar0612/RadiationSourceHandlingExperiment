using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MyVRPlayerRig : MonoBehaviour
{
    [Header("Model Transform")]

    [SerializeField]
    [Tooltip("model's lefthand tranform")]
    Transform m_LHand;
    /// <summary>
    /// model's lefthand tranform
    /// </summary>
    public Transform lHand
    {
        get => m_LHand;
        set => m_LHand = value;
    }

    [SerializeField]
    [Tooltip("model's righthand tranform")]
    Transform m_RHand;
    /// <summary>
    /// model's righthand tranform
    /// </summary>
    public Transform rHand
    {
        get => m_RHand;
        set => m_RHand = value;
    }

    [SerializeField]
    [Tooltip("model's headtranform")]
    Transform m_Head;
    /// <summary>
    /// model's headtranform
    /// </summary>
    public Transform head
    {
        get => m_Head;
        set => m_Head = value;
    }

    [Space]
    [Header("Controller")]

    [SerializeField]
    [Tooltip("Prefab model manager. Used to synchronize the transform of different parts of the model with the corresponding parts of the VR origin of the real scene.")]
    VRNetworkPlayerController m_VRPlayerController;
    /// <summary>
    /// Prefab model manager. 
    /// Used to synchronize the transform of different parts of the model with the corresponding parts of the VR origin of the real scene.
    /// </summary>
    public VRNetworkPlayerController vrPlayerController
    {
        get => m_VRPlayerController;
        set => m_VRPlayerController = value;
    }

    VRPlayerController m_VRPlayerCtrl;

    public VRPlayerController vrPlayerCtrl
    {
        get => m_VRPlayerCtrl;
        set => m_VRPlayerCtrl = value;
    }

    /// <summary> 渐变效果 </summary>
    private VRScreenFade vrScreenFade;

    [Space]
    [Header("VR IK")]

    Transform ikHeadTarget;
    
    Transform ikRightHandTarget;
    
    Transform ikLeftHandTarget;

    public void Awake()
    {
        if (vrScreenFade == null)
            vrScreenFade = (VRScreenFade)FindObjectOfType(typeof(VRScreenFade));
    }

    public void Start()
    {
        vrScreenFade.SetAlphaVar(1.0f, 0.0f);
        vrScreenFade.enabled = true;
    }

    private void NormalPlayerModleSync()
    {
        if (vrPlayerController)
        {
            vrPlayerController.head.position = head.transform.position;
            vrPlayerController.head.rotation = head.transform.rotation;

            vrPlayerController.lHand.position = lHand.transform.position;
            vrPlayerController.lHand.rotation = lHand.transform.rotation;

            vrPlayerController.rHand.position = rHand.transform.position;
            vrPlayerController.rHand.rotation = rHand.transform.rotation;
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

            var win = UIController.Get().GetWindow<VRExitWindow>(EWindowType.VRExitWindow) as VRExitWindow;
            
            // TODO..
            if (vrPlayerCtrl.ik.solver.leftArm.target != null && vrPlayerCtrl.ik.solver.leftArm.target.position.x != 0.0f && vrPlayerCtrl.ik.solver.leftArm.target.position.z != 0.0f)
            {
                vrPlayerCtrl.ik.solver.leftArm.positionWeight = 1.0f;
                vrPlayerCtrl.ik.solver.leftArm.rotationWeight = 0.8f;

                win.ShowText($"{vrPlayerCtrl.ik.solver.leftArm.target.position}，{ikLeftHandTarget.transform.position}");
            }

            if (vrPlayerCtrl.ik.solver.rightArm.target != null && vrPlayerCtrl.ik.solver.rightArm.target.position.x != 0.0f && vrPlayerCtrl.ik.solver.rightArm.target.position.z != 0.0f)
            {
                vrPlayerCtrl.ik.solver.rightArm.positionWeight = 1.0f;
                vrPlayerCtrl.ik.solver.rightArm.rotationWeight = 0.8f;
            }

            if (vrPlayerCtrl.ik.solver.leftArm.target != null && vrPlayerCtrl.ik.solver.leftArm.target.position.x == 0.0f && vrPlayerCtrl.ik.solver.leftArm.target.position.z == 0.0f)
            {
                vrPlayerCtrl.ik.solver.leftArm.positionWeight = 0.0f;
                vrPlayerCtrl.ik.solver.leftArm.rotationWeight = 0.0f;
            }

            if (vrPlayerCtrl.ik.solver.rightArm.target != null && vrPlayerCtrl.ik.solver.rightArm.target.position.x == 0.0f && vrPlayerCtrl.ik.solver.rightArm.target.position.z == 0.0f)
            {
                vrPlayerCtrl.ik.solver.rightArm.positionWeight = 0.0f;
                vrPlayerCtrl.ik.solver.rightArm.rotationWeight = 0.0f;
            }

            // TODO..
            // vrPlayerCtrl.isInitIK = true;
        }
    }

    void FixedUpdate()
    {
        //NormalPlayerModleSync();
        VRPlayerModleSync();
    }
}
