using Cysharp.Threading.Tasks;
using LitJson;

public class ConfigHelper
{
    public static async UniTask<T> SetConfigObject<T>(string filePath)
    {
        string SettingJson = await FileHelper.DownLoadTextFromServer(filePath);
        return JsonMapper.ToObject<T>(SettingJson);
    }
}