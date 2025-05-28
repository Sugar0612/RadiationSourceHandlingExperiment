
using Mirror;
using Telepathy;
using TMPro;
using UnityEngine;

public class UserWindow : WinNetBase, IWin
{
    public override EWindowType windowType => EWindowType.UserWindow;

    // 人数文本
    public TMP_Text personCountText;

    public TMP_Text messageText;

    // [SyncVar(hook = nameof(UpdateMessageOfUser))]
    // [HideInInspector] public string messageTextStr = "";

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

    public void UpdateMessageOfUser(string message)
    {
        Log.cinput("red", $"UpdateMessageOfUser: {message}");
        messageText.text += message + "\n";
    }

    // [Command(requiresAuthority = false)]
    // public void UpdateMessageOfUser(string _, string New)
    // {
    //     if (isServer)
    //     {
    //         Log.cinput("red", $"UpdateMessageOfUser: {New}");
    //         messageText.text += New + "\n";
    //     }
    // }
}