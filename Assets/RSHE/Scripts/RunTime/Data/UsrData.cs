
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UsrData
{
    public EIdentity identity = 0;
    public float Score = 0.0f;

    [SerializeField]
    public List<IncorrectData> IncorrectList = new List<IncorrectData>();

    public UsrData() { }

    public UsrData(EIdentity _identity, float score)
    {
        identity = _identity;
        Score = score;
        IncorrectList = new List<IncorrectData>();
    }

    public void AddIncorrectItem(IncorrectData incorrect)
    {
        IncorrectList.Add(incorrect);
    }
}
