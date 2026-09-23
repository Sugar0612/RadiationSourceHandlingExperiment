using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : NetworkBehaviour
{
    /// <summary> 教学视频在 Addressables 组 RSHE-Dynamic 中的地址 </summary>
    const string VideoAddress = "Video/FinalVideo";

    public VideoPlayer VideoPlayer;

    public RawImage VideoScreen;

    AsyncOperationHandle<VideoClip> _videoHandle;

    static VideoController _instance;

    public static VideoController Get()
    {
        if (_instance == null)
            _instance = FindObjectOfType<VideoController>();

        return _instance;
    }

    void Start()
    {
        // _vrVideoWindow = UIController.Get().GetWindow<VRVideoWindow>(EWindowType.VRVideoWindow) as VRVideoWindow;

        VideoPlayer = GetComponent<VideoPlayer>();
        VideoScreen.texture = VideoPlayer.targetTexture;
        VideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        VideoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());

        // 视频不再由场景序列化引用,改由 Addressables 异步加载(70MB 资产按需载入内存)
        _videoHandle = Addressables.LoadAssetAsync<VideoClip>(VideoAddress);
        _videoHandle.Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                VideoPlayer.clip = op.Result;
            }
            else
            {
                Log.cinput("red", $"[Addressables] 教学视频加载失败: {op.OperationException}");
            }
        };
    }

    void OnDestroy()
    {
        if (_videoHandle.IsValid())
            Addressables.Release(_videoHandle);
    }

    [Command(requiresAuthority = false)]
    public void CmdCtrlVideoState()
    {
        if (VideoPlayer.isPlaying)
            RpcPauseVideo();
        else
            RpcPlayVideo();
    }

    [ClientRpc]
    public void RpcCtrlVideoState()
    {
        if (VideoPlayer.isPlaying)
            VideoPlayer.Pause();
        else
            VideoPlayer.Play();
    }

    [ClientRpc] public void RpcPlayVideo() { if (VideoPlayer.clip != null) VideoPlayer.Play(); }

    [ClientRpc] public void RpcPauseVideo() => VideoPlayer.Pause();
}