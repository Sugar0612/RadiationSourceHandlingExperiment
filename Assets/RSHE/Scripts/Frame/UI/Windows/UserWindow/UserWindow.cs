
using Mirror;
using System.Collections.Generic;
using Telepathy;
using TMPro;
using UnityEngine;

public class UserWindow : WinBase
{
    // 人数文本
    public TMP_Text personCountText;

    public TMP_Text messageText;

    // [SyncVar(hook = nameof(UpdateMessageOfUser))]
    // [HideInInspector] public string messageTextStr = "";

    public UserItem userItemTemp;

    public Transform userItemParent;

    List<UserItem> userItemList = new List<UserItem>();

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        if (StaticGlobalVar.isPicoDevice == 0)
        {
            // gameObject.TryFindAndSetStatus("PersonCountText", true, out personCountText);
        }
    }

    public void ChangedpersonCountText(int personCount)
    {
        // Log.cinput("red", $"ChangedpersonCountTextZ: {personCount}");
        MyVRStaticVariables.personCount = personCount;
        personCountText.text = personCount.ToString();
    }

    // TODO...
    public void Init(string deviceID)
    {

    }
}