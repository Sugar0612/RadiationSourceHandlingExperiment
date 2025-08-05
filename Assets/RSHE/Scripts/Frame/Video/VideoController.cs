using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : NetworkBehaviour
{
    public VideoPlayer VideoPlayer;

    VRVideoWindow _vrVideoWindow;

    static VideoController _instance;

    public static VideoController Get()
    {
        if (_instance == null)
            _instance = FindObjectOfType<VideoController>();

        return _instance;
    }

    void Start()
    {
        _vrVideoWindow = UIController.Get().GetWindow<VRVideoWindow>(EWindowType.VRVideoWindow) as VRVideoWindow;

        VideoPlayer = GetComponent<VideoPlayer>();
        _vrVideoWindow.VideoScreen.texture = VideoPlayer.targetTexture;
        VideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        VideoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
    }

    [Command(requiresAuthority = false)]
    public void CmdCtrlVideoState()
    {
        if (VideoPlayer.isPlaying)
            RpcPauseVideo();
        else
            RpcPlayVideo();
    }

    [ClientRpc] public void RpcPlayVideo() => VideoPlayer.Play();

    [ClientRpc] public void RpcPauseVideo() => VideoPlayer.Pause();
}