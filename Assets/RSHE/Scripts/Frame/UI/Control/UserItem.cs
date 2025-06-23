using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserItem : MonoBehaviour
{
    [SerializeField] TMP_Text identityTx;

    [SerializeField] TMP_Text stateTx;

    public void Init(int _indentity, int _state) 
    {
        identityTx.text = ((EIdentity)_indentity).ToString();
        stateTx.text = m_UserStateDic[(EUserState)_state];
    }

    public void SetState(int _state)
    {
        stateTx.text = m_UserStateDic[(EUserState)_state]; ;
    }

    public enum EIdentity
    {
        A1 = 0, A2 = 1, A3 = 2,
        B1 = 3, B2 = 4, B3 = 5,
        C1 = 6, C2 = 7, C3 = 8,
        None = 9
    }

    public enum EUserState
    {
        Offline = 0,
        Online = 1,
        Neno = 2
    }

    Dictionary<EUserState, string> m_UserStateDic = new Dictionary<EUserState, string>() { { EUserState.Offline, "¿Îœﬂ" }, { EUserState.Online, "‘⁄œﬂ"} };
}