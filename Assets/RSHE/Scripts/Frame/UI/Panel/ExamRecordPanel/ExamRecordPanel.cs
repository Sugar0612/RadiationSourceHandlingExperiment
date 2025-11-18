using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamRecordPanel : MonoBehaviour
{
    public GameObject ExamItemTemplate;

    public Transform ExamItemParent;

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
                examItem.Init(item.time);
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

    public void OnDestroy()
    {
        Destroy();
    }
}
