using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class AudioController : NetworkBehaviour
{
    static AudioController _instance;

    public static AudioController Get()
    {
        if (_instance == null)
            _instance = FindObjectOfType<AudioController>();

        return _instance;
    }

    /// <summary> ÒôÆµ²¥·Å×é¼þ </summary>
    AudioSource _audioSource;

    public void Play(AudioClip clip)
    {
        if (clip != null)
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            if (_audioSource)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
            }
        }
    }
}
