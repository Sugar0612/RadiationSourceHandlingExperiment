using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Transporter : NetworkBehaviour
{
    /// <summary> 去放射源一 </summary>
    public Button GoPointOneButton;

    /// <summary> 去放射源二 </summary>
    public Button GoPointTwoButton;

    /// <summary> 一号线回收 </summary>
    public Button GoBackButtonOne;

    /// <summary> 二号线回收 </summary>
    public Button GoBackButtonTwo;

    public Animator ActionAnimator;

    public void Awake()
    {
        ActionAnimator.GetComponentInParent<Animator>();
    }

    public void Start()
    {
        GoPointOneButton.onClick.AddListener(() =>
        {
            CmdSetAnimatorBool("goPointOne", true);
            CmdSetAnimatorBool("goBackOne", false);
            GoBackButtonOne.SetButtonActive(true);
            GoPointOneButton.SetButtonActive(false);
            GoPointTwoButton.SetButtonActive(false);
        });

        GoPointTwoButton.onClick.AddListener(() =>
        {
            CmdSetAnimatorBool("goPointTwo", true);
            CmdSetAnimatorBool("goBackTwo", false);
            GoBackButtonTwo.SetButtonActive(true);
            GoPointOneButton.SetButtonActive(false);
            GoPointTwoButton.SetButtonActive(false);
        });

        GoBackButtonTwo.onClick.AddListener(() =>
        {
            CmdSetAnimatorBool("goPointTwo", false);
            CmdSetAnimatorBool("goBackTwo", true);
        });

        GoBackButtonOne.onClick.AddListener(() =>
        {
            CmdSetAnimatorBool("goPointOne", false);
            CmdSetAnimatorBool("goBackOne", true);
        });

        GoBackButtonOne.SetButtonActive(false);
        GoBackButtonTwo.SetButtonActive(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetAnimatorBool(string param, bool val)
    {
        RpcSetAnimatorBool(param, val);
    }

    [ClientRpc]
    public void RpcSetAnimatorBool(string param, bool val)
    {
        ActionAnimator?.SetBool(param, val);
    }
}
