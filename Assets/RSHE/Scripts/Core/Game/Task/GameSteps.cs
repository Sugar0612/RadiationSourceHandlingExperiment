using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

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

    /// <summary> 步骤存放容器，一个步骤里面可能有很多个小任务 </summary>
    [SerializeField]
    public List<GameTask> stepsList = new List<GameTask>();

    /// <summary> 当前步骤 </summary>
    GameTask currStep { get { return stepsList?[stepIdx]; } }

    /// <summary> 当前任务 </summary>
    GameTaskItem currTask { get { return currTaskList?[taskIdx]; } }

    /// <summary> 当前任务列表 </summary>
    List<GameTaskItem> currTaskList { get { return currStep.tasksList; } }

    int stepIdx = 0; // 大步骤索引

    int taskIdx = 0; // 小任务索引

    public int[,] TaskTable = new int[100, 100];

    public Dictionary<TaskName, int[]> TaskPosDic = new Dictionary<TaskName, int[]>();

    public bool isTopTask { get { return currStep.tasksList.Count - 1 == taskIdx; } }

    #endregion

    public int GetTaskIdx() => taskIdx;

    private void Start()
    {
        InitTaskTable();
    }


    public bool CheckTaskGoRun(TaskName taskname)
    {
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            //int stepIdx = TaskPosDic[task][0];
            //int taskIdx = TaskPosDic[task][1];
            //Log.cinput("yellow", $"task: {task.ToString()}: {TaskTable[stepIdx, taskIdx]}");

            if (task == taskname)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
                return false;
        }

        if (!GameSteps.Get().IsCheckTaskFinished(taskname))
        {
            GameSteps.Get().Run();
        }

        return true;
    }

    public void InitTaskTable()
    {
        for (int i = 0; i < stepsList.Count; ++i)
        {
            GameTask gameTask = stepsList[i];
            for (int j = 0; j < gameTask.tasksList.Count; ++j)
            {
                GameTaskItem task = gameTask.tasksList[j];
                TaskName taskName = (TaskName)Enum.Parse(typeof(TaskName), task.taskName);
                task.StepPos = i;
                task.TaskPos = j;
                TaskTable[i, j] = 0;
                TaskPosDic.Add(taskName, new int[2] { i, j });
            }
        }
    }

    public bool IsCheckTaskFinished(TaskName taskName)
    {
        int stepIdx = TaskPosDic[taskName][0];
        int taskIdx = TaskPosDic[taskName][1];
        return TaskTable[stepIdx, taskIdx] == 1;
    }

    public void SetTaskFinished(TaskName taskName)
    {
        int stepIdx = TaskPosDic[taskName][0];
        int taskIdx = TaskPosDic[taskName][1];
        TaskTable[stepIdx, taskIdx] = 1;
    }

    /// <summary>
    /// 开始下一个任务
    /// </summary>
    public void Next()
    {
        if (taskIdx + 1 < currTaskList.Count)
        {
            taskIdx++;
            RunStart();
            return;
        }

        NextStep();
    }

    /// <summary>
    /// 开始下一个步骤
    /// </summary>
    void NextStep()
    {
        if (stepIdx + 1 < stepsList.Count)
        {
            taskIdx = 0;
            stepIdx++;
            RunStart();
            return;
        }
    }

    /// <summary> 执行开始任务 </summary>
    public void RunStart()
    {
        // Log.cinput("yellow", $"currTask: {currTask.taskName}  StepPos: {currTask.StepPos}, taskPos: {currTask.TaskPos}, TaskTable: {TaskTable[currTask.StepPos, currTask.TaskPos]}");
        currTask.RunStart();
    }

    /// <summary> 执行中间任务 </summary>
    public void RunEnd()
    {
        //Log.cinput("yellow", $"currTask: StepPos: {currTask.StepPos}, taskPos: {currTask.TaskPos}, TaskTable: {TaskTable[currTask.StepPos, currTask.TaskPos]}");
        currTask.RunEnd();
    }

    /// <summary> 执行结束任务 </summary>
    public void Run()
    {
        //Log.cinput("yellow", $"currTask: StepPos: {currTask.StepPos}, taskPos: {currTask.TaskPos}, TaskTable: {TaskTable[currTask.StepPos, currTask.TaskPos]}");
        currTask.RunTask();
    }

    /// <summary> 设置步骤索引 </summary>
    public void SetStepIndex(int targetStepIdx) 
    {
        if (targetStepIdx >= 0 && targetStepIdx < stepsList.Count)
        {
            stepIdx = targetStepIdx;
        }
    }
}
