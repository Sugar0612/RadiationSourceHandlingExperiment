
using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public class ExamData
{
    public string time;

    [SerializeField]
    public List<UsrData> UsrList = new List<UsrData>();

    public ExamData()
    {
        time = Utility.GetLocalTime();
        UsrList = new List<UsrData>();
        foreach (EIdentity identity in System.Enum.GetValues(typeof(EIdentity)))
        {
            if (identity == 0) continue;
            UsrData usrData = new UsrData(identity, 100.0f);
            AddUsrData(usrData);
        }
    }

    public void AddUsrData(UsrData usrdata)
    {
        UsrList.Add(usrdata);
    }

    public UsrData Find(EIdentity identity)
    {
        return UsrList.Find(_ => _.identity == identity);
    }

    public void Deduction(EIdentity identity, float score, string reason)
    {
        UsrData targetUsr = Find(identity);
        targetUsr.Score -= score;

        IncorrectData incorr = new IncorrectData(reason, score);
        targetUsr.AddIncorrectItem(incorr);
    }
}
