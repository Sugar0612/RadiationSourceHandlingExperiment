using UnityEngine;

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

    public static void SetGameObjectMeshActive(this GameObject obj, bool active)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var render in renderers)
        {
            render.enabled = active;
        }
    }
}