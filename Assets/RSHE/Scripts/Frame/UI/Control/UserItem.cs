using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour
{
    [SerializeField] TMP_Text identityTx;

    [SerializeField] Image _stateImg;

    [SerializeField] TMP_Text stateTx;

    [HideInInspector] public UserConfig userCfg = new UserConfig();

    public void Init(UserConfig _userCfg) 
    {
        userCfg = _userCfg;
        identityTx.text = (userCfg.Identity).ToString();
        stateTx.text = m_UserStateDic[0];
    }

    public void SetState(EUserState _state)
    {
        stateTx.text = m_UserStateDic[_state];
        Utility.LoadImageFromAddressables(_stateImg, m_StateImgDic[_state]);
    }

    Dictionary<EUserState, string> m_UserStateDic = new Dictionary<EUserState, string>() { { EUserState.Offline, "¿Îœﬂ" }, { EUserState.Online, "‘⁄œﬂ"} };

    Dictionary<EUserState, string> m_StateImgDic = new Dictionary<EUserState, string>() { { EUserState.Offline, "UI/Ctrl/Offline" }, { EUserState.Online, "UI/Ctrl/Online" } };
}