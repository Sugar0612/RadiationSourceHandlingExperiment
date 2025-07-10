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
    /// <summary>
    /// Player Name ui component in Scene.
    /// </summary>
    public TMP_Text textPlayerName;

    [HideInInspector] public VRIK ik;

    MyVRPlayerRig playerRig;

    public GameObject humanModel;

    public GameObject helmetModel;

    /// <summary>
    /// Player name variable.
    /// </summary>
    [SyncVar(hook = nameof(OnNameChangedHook))]
    string playerName;

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

    public void ScaleInitialization()
    {
        if (isInitScale) return;

        if (isLocalPlayer && check())
        {
            float scaleMlp = 1.0f;

            float rootPosY = ik.references.root.position.y;

            float sizeF = (ik.solver.spine.headTarget.position.y - rootPosY) / (ik.references.head.position.y - rootPosY);
            Log.cinput("red", $"headTargetY£º {ik.solver.spine.headTarget.position.y}, rootPosY: {rootPosY}, headY: {ik.references.head.position.y}");

            float magn = (sizeF * scaleMlp);
            if (magn > 0.0f)
            {
                Log.cinput("yellow", $"@@ magn£º {magn}");
                Log.cinput("yellow", $"@@ ScaleInitialization @@");
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
    
    private void Update()
    {
        ScaleInitialization();
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
