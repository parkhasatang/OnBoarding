using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ResourceManager : Singleton<ResourceManager>
{
    private readonly Dictionary<Type, Dictionary<string, UnityEngine.Object>> resourceMap = new();

    protected override void Awake()
    {
        base.Awake();
        // Ÿ�Ժ� ���ҽ� �ε�
        LoadResourcesByType<TextAsset>("MonsterData");
        LoadResourcesByType<MonsterStateController>("GameObject");
    }

    private void LoadResourcesByType<T>(string path) where T : UnityEngine.Object
    {
        T[] resources = Resources.LoadAll<T>($"Load/{path}");
        if (resources.Length > 0)
        {
            if (!resourceMap.ContainsKey(typeof(T)))
            {
                resourceMap[typeof(T)] = new Dictionary<string, UnityEngine.Object>();
            }

            foreach (T resource in resources)
            {
                resourceMap[typeof(T)][resource.name] = resource;
            }
        }
        else
        {
            Debug.LogWarning($"No resources of type {typeof(T).Name} found in {path}.");
        }
    }

    public T GetResource<T>(string name) where T : UnityEngine.Object
    {
        Type type = typeof(T);
        if (resourceMap.ContainsKey(type) && resourceMap[type].ContainsKey(name))
        {
            return resourceMap[type][name] as T;
        }

        Debug.LogWarning("No Resources of type " + type.Name + " found with name " + name);

        return null; // ���ҽ��� ���� ��� null ��ȯ
    }
}