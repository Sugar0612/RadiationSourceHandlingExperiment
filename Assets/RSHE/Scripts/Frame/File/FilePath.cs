using UnityEngine;

public class FilePath
{
    /// <summary> 获取玩家身份列表 </summary>
    public static readonly string UserConfigListPath = Application.streamingAssetsPath + "/Config/UserConfigList.json";

    /// <summary> 获取本地玩家的身份 </summary>
    public static readonly string LocalUserIdentityPath = Application.streamingAssetsPath + "/Config/LocalUserIdentity.json";

    /// <summary> 获取考试数据路径 </summary>
    public static readonly string examDataPath = Application.persistentDataPath + "/ExamList.json";
}
