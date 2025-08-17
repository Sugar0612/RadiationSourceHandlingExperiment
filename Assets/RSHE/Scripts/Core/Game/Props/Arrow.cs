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

    void Start()
    {
        SetActive(IsActive);

        _animator = gameObject.GetComponentInChildren<Animator>();
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
