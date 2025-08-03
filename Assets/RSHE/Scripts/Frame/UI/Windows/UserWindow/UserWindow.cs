
using Mirror;
using System.Collections;
using System.Collections.Generic;
using Telepathy;
using TMPro;
using UnityEngine;
using static UserItem;

public class UserWindow : WinBase
{
    public TMP_Text LogText; // 在Inspector中拖拽赋值

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

        if (!Config.Get().PicoDevice)
        {
            InitList();
        }
    }

    public void ChangedpersonCountText(int personCount)
    {
        // Log.cinput("red", $"ChangedpersonCountTextZ: {personCount}");
        StaticGlobalVar.PersonCount = personCount;
        personCountText.text = personCount.ToString();
    }

    /// <summary>
    /// 初始化列表
    /// </summary>
    void InitList()
    {
        StartCoroutine(Config.Get().GetUserConfigList(arg =>
        {
            userItemList.Clear();
            List<UserConfig> configList = arg;
            // Debug.LogError($"configList Count: {configList.Count}, {FilePath.UserConfigPath}");
            LogText.text += $"@@@ configList Count: {configList.Count}.\n";

            for (int i = 0; i < configList.Count; ++i)
            {
                var itemClone = GameObject.Instantiate(userItemTemp, userItemParent);
                itemClone.Init(configList[i]);
                itemClone.gameObject.SetActive(true);
                userItemList.Add(itemClone);
            }
        }));
    }

    /// <summary>
    /// 改变用户登录状态
    /// </summary>
    public void SetItemState(EIdentity identity, EUserState state)
    {
        UserItem target = userItemList.FindUserItem(identity);
        if (target != null)
        {
            LogText.text += $"{identity.ToString()} Item not NULL!\n";
            target.SetState(state);
        }
        else
        {
            LogText.text += $"{identity.ToString()} Item NULL!\n";
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < userItemList.Count; ++i)
        {
            userItemList[i].gameObject.SetActive(false);
            Destroy(userItemList[i]);
        }
        userItemList.Clear();
    }
}

public static class UserListExtensions
{
    public static UserItem FindUserItem(this List<UserItem> list, EIdentity identity)
    {
        UserItem item = new UserItem();

        item = list.Find(x => x.userCfg.Identity == identity);

        return item;
    }
}