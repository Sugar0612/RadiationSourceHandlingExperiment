using DG.Tweening;
using Mirror;
using Mirror.Examples.Basic;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class VRPlayerController : NetworkBehaviour
{
#region 玩家组件

    /// <summary> Player Name ui component in Scene. </summary>
    public TMP_Text textPlayerName;

    [HideInInspector] public VRIK ik;

    MyVRPlayerRig playerRig;

#endregion

#region 玩家参数

    /// <summary> Player name variable. </summary>
    [SyncVar(hook = nameof(OnNameChangedHook))]
    string playerName;

    [SyncVar]
    /// <summary> 身份 </summary>
    public EIdentity identity = EIdentity.None;

#endregion

    bool isInitScale = false;

    private void Start()
    {
        ik = GetComponent<VRIK>();

        if (!isLocalPlayer && ik)
        {
            ik.solver.spine.positionWeight = 0.0f;
            ik.solver.spine.rotationWeight = 0.0f;

            ik.solver.rightArm.positionWeight = 0.0f;
            ik.solver.rightArm.rotationWeight = 0.0f;

            ik.solver.leftArm.positionWeight = 0.0f;
            ik.solver.leftArm.rotationWeight = 0.0f;

            ik.enabled = false;

            Animator anim = GetComponent<Animator>();
            if (anim)
            {
                anim.enabled = false;
            }
        }

        if (isServer && isLocalPlayer)
            gameObject.SetActive(false);

        if (isLocalPlayer)
        {
            SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.sharedMesh = null;
            }
        }

        identity = Config.Get().GetIdentityBaseOnDeviceID(SystemInfo.deviceUniqueIdentifier);
    }

    public override void OnStartLocalPlayer()
    {
        Initialized();

        // gameObject.SetActive(false);

        if (VRStaticVariables.playerName != "")
            CmdSetupName(VRStaticVariables.playerName + netId);
        else
            CmdSetupName("Player" + netId);
    }

    public void Initialized()
    {
        if (playerRig == null)
        {
            playerRig = FindObjectOfType<MyVRPlayerRig>();
            if (playerRig != null)
            {
                // Log.cinput("yellow", "@@@@@@@@@ Initialized playerrig vrcontorller successed.");
                playerRig.vrPlayerCtrl = this;
                // ScaleInitialization();
            }
        }
    }

    public IEnumerator ScaleInitialization()
    {
        if (isInitScale) yield break;

        yield return new WaitForSeconds(1.0f);

        if (isLocalPlayer && check())
        {
            float scaleMlp = 1.0f;

            float sizeF = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);

            float magn = (sizeF * scaleMlp);

            if (magn > 0.0f && ik.references.root.localScale * magn != new Vector3(0f, 0f, 0f))
            {
                // Log.cinput("yellow", $"@@@ headTarget: {ik.solver.spine.headTarget.position.y}, root: {ik.references.root.position.y}，head : {ik.references.head.position.y}");
                ik.references.root.localScale *= magn;
                isInitScale = true;
            }
        }
    }

    public bool check()
    {
        return ik.solver.spine.headTarget != null && ik.solver.spine.headTarget.position != null &&
                ik.references.root != null && ik.references.root.position != null &&
                ik.references.head != null && ik.references.head.position != null;
    }
    
    private void FixedUpdate()
    {
        StartCoroutine(ScaleInitialization());
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
    /// <param name="_name"></param>
    [Command]
    public void CmdSetupName(string _name)
    {
        playerName = _name;
    }
}
