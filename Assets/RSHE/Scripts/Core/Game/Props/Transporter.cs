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
    /// <summary> 背景图片 </summary>
    public Image BGImage;

    /// <summary> 到达Text </summary>
    public TextMeshProUGUI ArriveText;

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
            SetActive(false);
        });

        GoPointTwoButton.onClick.AddListener(() =>
        {
            CmdSetAnimatorBool("goPointTwo", true);
            CmdSetAnimatorBool("goBackTwo", false);
            SetActive(false);
        });

        GoBackButtonTwo.onClick.AddListener(() =>
        {
            CmdSetAnimatorBool("goPointTwo", false);
            CmdSetAnimatorBool("goBackTwo", true);
            SetActive(false);
        });

        GoBackButtonOne.onClick.AddListener(() =>
        {
            CmdSetAnimatorBool("goPointOne", false);
            CmdSetAnimatorBool("goBackOne", true);
            SetActive(false);
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


    void SetActive(bool active)
    {
        GoBackButtonOne.SetButtonActive(active);
        GoBackButtonTwo.SetButtonActive(active);
        GoPointOneButton.SetButtonActive(active);
        GoPointTwoButton.SetButtonActive(active);

        if (ArriveText) ArriveText.text = active ? "目的地:" : "";
        if (BGImage) BGImage.enabled = active;
    }

    public void ArriveOnePointEvent()
    {
        GoBackButtonOne.SetButtonActive(true);
        if (ArriveText) ArriveText.text = "目的地:";
        if (BGImage) BGImage.enabled = true;
    }

    public void ArriveTwoPointEvent()
    {
        GoBackButtonTwo.SetButtonActive(true);
        if (ArriveText) ArriveText.text = "目的地:";
        if (BGImage) BGImage.enabled = true;
    }
}
