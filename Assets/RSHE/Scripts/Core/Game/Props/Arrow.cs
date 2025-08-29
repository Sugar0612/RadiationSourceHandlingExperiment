using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool IsActive;

    public ArrowType Type;

    Animator _animator;

    Dictionary<ArrowType, string> _animParamDic = new Dictionary<ArrowType, string>() { { ArrowType.None, "isHor" }, { ArrowType.Horizontal, "isHor" }, { ArrowType.Vertical, "isVer" }, };

    private void Awake()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        gameObject.SetActive<Renderer>(false);
    }

    public Arrow Clone()
    {
        Arrow copy = new Arrow();
        copy.IsActive = this.IsActive;
        copy.Type = this.Type;
        copy._animator = this._animator;
        return copy;
    }

    public void SetActive(bool active)
    {
        gameObject.SetRendererEnable(active);
        if (active)
            Play();
        else
            Pause();
    }

    public void Play()
    {
        if (_animator)
        {
            _animator.SetBool(_animParamDic[Type], true);
        }
    }

    public void Pause()
    {
        if (_animator)
        {
            _animator.SetBool(_animParamDic[Type], false);
        }
    }
}
