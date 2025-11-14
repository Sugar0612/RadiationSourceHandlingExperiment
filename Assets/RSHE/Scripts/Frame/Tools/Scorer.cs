using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Scorer : MonoBehaviour
{
    public List<ExamData> ExamList = new List<ExamData>();

    public ExamData currExamData;

    private static Scorer m_instance;

    public static Scorer Get()
    {
        if (m_instance == null)
        {
            GameObject go = new GameObject(typeof(Scorer).ToString());
            m_instance = go.AddComponent<Scorer>();
            go.name = typeof(Scorer).ToString();
        }
        return m_instance;
    }

    public void Awake()
    {
        Register();
        Init();
    }

    public void Register()
    {
        currExamData = new ExamData();
    }

    /// <summary> 
    /// </summary>
    public void Init()
    {
        StartCoroutine(Utility.ReadFile(FilePath.examDataPath, (content) =>
        {
            Log.cinput("green", $"读取到的文件: {content}");
            if (content.Count() == 0) return;
            ExamList = JsonMapper.ToObject< List<ExamData>> (content);
        }));
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    public void Save(Action callback)
    {
        ExamList.Add(currExamData);
        string newData = JsonMapper.ToJson(ExamList);
        Log.cinput("green", $"Save this data string: {newData}");

        if (Utility.OverwriteJSONFile(FilePath.examDataPath, newData))
        {
            callback();
        }
    }

    /// <summary>
    /// 减分
    /// </summary>
    public void Deduction(TaskName task, EIdentity identity, float score, PromptType reason)
    {
        string s_reason = MessPromp.AssPrompt(task, reason);
        currExamData.Deduction(identity, score, s_reason);
        Log.cinput("red", $"reason: {s_reason}, identity：{identity}, score:{score}");
    }

    public void Spawn() { }
}
