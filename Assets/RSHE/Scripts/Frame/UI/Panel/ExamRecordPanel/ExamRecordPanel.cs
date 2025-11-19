using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamRecordPanel : MonoBehaviour
{
    #region UI Component

    public GameObject ExamItemTemplate;

    public Transform ExamItemParent;

    public ExamUsrPanel ExamUsrPanelObject;

    #endregion
    List<ExamItem> examItemList = new List<ExamItem>();

    private bool isInit = false;

    void Init()
    {
        StartCoroutine(InitProcess());
    }

    IEnumerator InitProcess()
    {
        if (!Config.Get().PicoDevice)
        {
            yield return new WaitUntil(() => Scorer.Get().isSpawned == true);

            foreach (var item in Scorer.Get().ExamList)
            {
                ExamItem examItem = GameObject.Instantiate(ExamItemTemplate, ExamItemParent).GetComponent<ExamItem>();
                examItem.Init(item.time, this);
                examItemList.Add(examItem);
            }

            isInit = true;
        }
    }

    void Destroy()
    {
        for (int i = 0; i < examItemList.Count; ++i)
        {
            if (examItemList[i] != null)
            {
                examItemList[i].SetActive(false);
            }
        }
        examItemList.Clear();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        if (active) Init();
        else Destroy();
    }

    public void ShowExamUsrPanel(string examTime)
    {
        Log.cinput("green", $"@@@ ShowExamUsrPanel： {examTime}");
        ExamData data = Scorer.Get().Find(examTime);
        ExamUsrPanelObject.Init(data.UsrList);
    }

    public void OnDestroy()
    {
        Destroy();
    }
}
