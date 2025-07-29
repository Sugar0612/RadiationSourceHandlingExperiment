using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Timer
{
    private static TimerComponent _component;

    /// <summary>
    /// 延迟执行一个方法
    /// </summary>
    public static void Delay(float delay, Action callback)
    {
        if (delay <= 0)
        {
            callback?.Invoke();
            return;
        }

        EnsureComponentExists();
        _component.StartCoroutine(DelayRoutine(delay, callback));
    }

    /// <summary>
    /// 使用真实时间延迟执行一个方法（不受Time.timeScale影响）
    /// </summary>
    public static void DelayRealtime(float delay, Action callback)
    {
        if (delay <= 0)
        {
            callback?.Invoke();
            return;
        }

        EnsureComponentExists();
        _component.StartCoroutine(DelayRealtimeRoutine(delay, callback));
    }

    private static IEnumerator DelayRoutine(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }

    private static IEnumerator DelayRealtimeRoutine(float delay, Action callback)
    {
        yield return new WaitForSecondsRealtime(delay);
        callback?.Invoke();
    }

    private static void EnsureComponentExists()
    {
        if (_component == null)
        {
            GameObject timerObject = new GameObject("TimerService");
            _component = timerObject.AddComponent<TimerComponent>();
            UnityEngine.Object.DontDestroyOnLoad(timerObject);
        }
    }

    /// <summary>
    /// 内部组件用于在场景中承载协程
    /// </summary>
    private class TimerComponent : MonoBehaviour
    {
        void OnDestroy()
        {
            // 组件销毁时清理静态引用
            Timer._component = null;
        }
    }
}