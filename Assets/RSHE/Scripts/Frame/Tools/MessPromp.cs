using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MessPromp
{
    static Dictionary<PromptType, string> PromptStateDic = new Dictionary<PromptType, string>()
    {
        {PromptType.WearClothing, "未穿戴防护服！请立即返回穿戴！" },
        {PromptType.DataError, "提交数据不在正常范围！" },
        {PromptType.TaskOrderWrong, "请完成前提任务，再来完成该任务！" }
    };

    public static string Prompt(PromptType type)
    {
        return PromptStateDic[type];
    }
}
