using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserItem : MonoBehaviour
{
    [SerializeField] TMP_Text identityTx;

    [SerializeField] TMP_Text stateTx;

    [HideInInspector] public UserConfig userCfg = new UserConfig();

    public void Init(UserConfig _userCfg) 
    {
        userCfg = _userCfg;
        identityTx.text = (userCfg.identity).ToString();
        stateTx.text = m_UserStateDic[0];
    }

    public void SetState(EUserState _state)
    {
        stateTx.text = m_UserStateDic[_state];
    }

    Dictionary<EUserState, string> m_UserStateDic = new Dictionary<EUserState, string>() { { EUserState.Offline, "¿Îœﬂ" }, { EUserState.Online, "‘⁄œﬂ"} };
}