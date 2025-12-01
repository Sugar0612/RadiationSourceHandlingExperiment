using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public static class GameObjectExtensions
{
    public static void TryFindAndSetStatus<T>(this GameObject parent, string name, bool active, out T component) where T : MonoBehaviour
    {
        GameObject obj = GameObject.Find(name);
        if (obj != null)
        {
            component = obj.GetComponent<T>();
            if (component != null)
            {
                component.gameObject.SetActive(active);
            }
            else
            {
                Debug.LogError($"Component of type {typeof(T)} not found on {name}");
            }
        }
        else
        {
            Debug.LogError($"GameObject with name {name} not found");
            component = null;
        }
    }

    public static void SetActive<T>(this GameObject gameObject, bool enabled) where T : Component
    {
        if (gameObject == null) return;
        //Log.cinput("yellow", $"SetActive: {typeof(T)} : {enabled}");
        T[] components = gameObject.GetComponentsInChildren<T>();
        foreach (T comp in components)
        {
            var prop = comp.GetType().GetProperty("enabled");
            if (prop != null && prop.CanWrite && prop.PropertyType == typeof(bool))
            {
                //Log.cinput("yellow", $"gameObject enabled: {typeof(T)} {gameObject.name} : {enabled}");
                prop.SetValue(comp, enabled, null);
            }
        }
    }

    //public static void SetActive<T> (this GameObject _this, bool active) where T : Behaviour
    //{
    //    T[] tArray = _this.gameObject.GetComponentsInChildren<T>();
    //    foreach (var t in tArray)
    //        t.enabled = active;
    //}

    public static void SetRendererEnable(this GameObject obj, bool enable)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var render in renderers)
        {
            render.enabled = enable;
        }
    }

    public static void SetColliderEnable(this GameObject obj, bool enable)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = enable;
        }
    }

    public static void SetActiveForTheUI<T>(this GameObject obj, bool active) where T : MonoBehaviour
    {
        T[] ts = obj.gameObject.GetComponentsInChildren<T>();
        foreach (var item in ts)
            item.gameObject.GetComponent<T>().enabled = active;
    }
}
