using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MessPromp
{
    static Dictionary<PromptType, string> PromptStateDic = new Dictionary<PromptType, string>()
    {
        {PromptType.WearClothing, "未穿戴防护服！请立即返回穿戴！" },
        {PromptType.DataError, "提交数据不在正常范围！" },
        {PromptType.TaskOrderWrong, "请完成前提任务，再来完成该任务！" },
        {PromptType.TaskIsFinished, "该任务已完成，请勿重复！" },
        {PromptType.PropWrong, "不是本次任务对应的道具！" },
        {PromptType.TActionWrong, "运输车行为异常！" }
    };

    static Dictionary<PromptType, string> assPromptStateDic = new Dictionary<PromptType, string>()
    {
        {PromptType.WearClothing, "未穿戴防护服!" },
        {PromptType.DataError, "提交数据不在正常范围!" },
        {PromptType.TaskOrderWrong, "任务步骤顺序错误!" },
        {PromptType.PropWrong, "道具使用错误!" },
        {PromptType.TActionWrong, "运输车使用错误!" }
    };

    static Dictionary<TaskName, string> TaskEnum2Str = new Dictionary<TaskName, string>()
    {
        {TaskName.T1, "任务一" }, {TaskName.T2, "任务二" }, {TaskName.T3, "任务三" }, {TaskName.T4, "任务四" }, {TaskName.T5, "任务五" }, {TaskName.T6, "任务六" }, {TaskName.T7, "任务七" }, {TaskName.T8, "任务八" }, {TaskName.T9, "任务九" }, {TaskName.T10, "任务十" }, {TaskName.T11, "任务十一" }, {TaskName.T12, "任务十二" }, {TaskName.T13, "任务十三" }
    };

    public static string Prompt(PromptType type)
    {
        return PromptStateDic[type];
    }

    public static string AssPrompt(TaskName task, PromptType type)
    {
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.Append($"{TaskEnum2Str[task]}: {assPromptStateDic[type]}");
        return strBuilder.ToString();
    }
}
