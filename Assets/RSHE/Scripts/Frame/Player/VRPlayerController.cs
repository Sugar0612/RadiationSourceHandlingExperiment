using DG.Tweening;
using Mirror;
using Mirror.Examples.Basic;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Start()
    {
        Log.cinput("yellow", "@@@@@@@@@@@@@@@@@@@@@@ VRPlayerController Start.");
        ik = GetComponent<VRIK>();

        if (!isLocalPlayer && ik)
        {
            Log.cinput("yellow", "@@@@@@@@@ !isLocalPlayer && ik.");
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
        Log.cinput("yellow", "@@@@@@@@@@@@@@@@@@@@@@ VRPlayerController OnStartLocalPlayer.");
        Initialized();

        // gameObject.SetActive(false);

        if (VRStaticVariables.playerName != "")
            CmdSetupName(VRStaticVariables.playerName + netId);
        else
            CmdSetupName("Player" + netId);

        // humanModel.SetActive(false);
        // helmetModel.SetActive(false);
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
