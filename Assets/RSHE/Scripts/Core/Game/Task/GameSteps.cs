using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GameSteps : NetworkBehaviour
{
    static GameSteps m_Instance = null;

    public static GameSteps Get()
    {
        if (m_Instance == null)
        {
            m_Instance = GameObject.FindObjectOfType<GameSteps>();
        }
        return m_Instance;
    }

    #region 任务步骤参数
    /// <summary>
    /// 步骤存放容器
    /// 一个步骤里面可能有很多个小任务
    /// </summary>
    [SerializeField]
    public List<GameTask> stepsList = new List<GameTask>();
 
    /// <summary> 当前步骤 </summary>
    GameTask currStep { get { return stepsList?[stepIdx]; } }

    /// <summary> 当前任务 </summary>
    GameTaskItem currTask { get { return currTaskList?[taskIdx]; } }

    /// <summary> 当前任务列表 </summary>
    List<GameTaskItem> currTaskList { get { return currStep.tasksList; } }

    [SyncVar]
    int stepIdx = 0; // 大步骤索引

    [SyncVar]
    int taskIdx = 0; // 小任务索引
    #endregion

    private void Start()
    {
        GameSteps.Get().ExecuteCurrentTask();
    }

    /// <summary>
    /// 开始下一个任务
    /// </summary>
    public void NextTaskIndex()
    {
        if (taskIdx + 1 < currTaskList.Count)
        {
            taskIdx++;
            ExecuteCurrentTask();
            return;
        }

        NextStepIndex();
    }

    /// <summary>
    /// 开始下一个步骤
    /// </summary>
    public void NextStepIndex()
    {
        if (stepIdx + 1 < stepsList.Count)
        {
            stepIdx++;
            ExecuteCurrentTask();
            return;
        }
    }

    /// <summary> 设置步骤索引 </summary>
    public void SetStepIndex(int targetStepIdx) { if (targetStepIdx >= 0 && targetStepIdx < stepsList.Count) stepIdx = targetStepIdx; }

    /// <summary> 执行任务 </summary>
    public void ExecuteCurrentTask()
    {
        currTask.task.Invoke();
    }
}