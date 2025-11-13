using UnityEngine;

public class FilePath
{
    /// <summary> 获取玩家身份列表 </summary>
    public static string UserConfigListPath = Application.streamingAssetsPath + "/Config/UserConfigList.json";

    /// <summary> 获取本地玩家的身份 </summary>
    public static string LocalUserIdentityPath = Application.streamingAssetsPath + "/Config/LocalUserIdentity.json";

    /// <summary> 获取考试数据路径 </summary>
    public static string examDataPath = Application.streamingAssetsPath + "/Config/ExamList.json";
}
