using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// A simple, *single-threaded*, service locator appropriate for use with Unity.
/// </summary>


public class ServiceLocatorLazy
{
    private static ServiceLocatorLazy instance;

    private Dictionary<Type, Type> singletons = new Dictionary<Type, Type>();
    private Dictionary<Type, Type> transients = new Dictionary<Type, Type>();
    private Dictionary<Type, object> singletonInstances = new Dictionary<Type, object>();
    private Dictionary<Type, object> transientsInstances = new Dictionary<Type, object>();

    private Dictionary<Type, string> singletonsPrefabName = new Dictionary<Type, string>();

    static ServiceLocatorLazy()
    {
        instance = new ServiceLocatorLazy();
    }

    protected ServiceLocatorLazy()
    {
    }

    public static ServiceLocatorLazy Instance
    {
        get { return instance; }
    }

    public bool IsEmpty
    {
        get { return singletons.Count == 0 && transients.Count == 0; }
    }

    public void HandleNewSceneLoaded()
    {
        List<IMultiSceneSingleton> multis = new List<IMultiSceneSingleton>();
        foreach (KeyValuePair<Type, object> pair in singletonInstances)
        {
            IMultiSceneSingleton multi = pair.Value as IMultiSceneSingleton;
            if (multi != null)
                multis.Add(multi);
        }

        foreach (var multi in multis)
        {
            MonoBehaviour behavior = multi as MonoBehaviour;
            if (behavior != null)
                behavior.StartCoroutine(multi.HandleNewSceneLoaded());
        }
    }

    public void Reset()
    {
        List<Type> survivorRegisteredTypes = new List<Type>();
        List<object> survivors = new List<object>();
        foreach (KeyValuePair<Type, object> pair in singletonInstances)
        {
            if (pair.Value is IMultiSceneSingleton)
            {
                survivors.Add(pair.Value);
                survivorRegisteredTypes.Add(pair.Key);
            }
        }

        singletons.Clear();
        transients.Clear();
        singletonInstances.Clear();
        singletonsPrefabName.Clear();
        for (int i = 0; i < survivors.Count; i++)
        {
            singletonInstances[survivorRegisteredTypes[i]] = survivors[i];
            singletons[survivorRegisteredTypes[i]] = survivors[i].GetType();
        }
    }

    public void RegisterSingleton<TConcrete>()
    {
        singletons[typeof(TConcrete)] = typeof(TConcrete);
    }

    private void RegisterSingletonReflectOne<TConcrete>()
    {
        singletons[typeof(TConcrete)] = typeof(TConcrete);
    }

    private TConcrete RegisterSingletionAndResolve<TConcrete>() where TConcrete : class
    {
        singletons[typeof(TConcrete)] = typeof(TConcrete);
        return Resolve<TConcrete>();

    }

    public void RegisterSingleton<TAbstract, TConcrete>()
    {
        singletons[typeof(TAbstract)] = typeof(TConcrete);
    }

    private void RegisterSingletonReflectTwo<TAbstract, TConcrete>()
    {
        singletons[typeof(TAbstract)] = typeof(TConcrete);
    }

    private void RegisterSingletonReflectPrefab<TAbstract, TConcrete>(string prefabName)
    {
        singletons[typeof(TAbstract)] = typeof(TConcrete);
        singletonsPrefabName[typeof(TAbstract)] = prefabName;
    }

    private void RegisterSingleton<TAbstract, TConcrete>(string prefabName)
    {
        singletons[typeof(TAbstract)] = typeof(TConcrete);
        singletonsPrefabName[typeof(TAbstract)] = prefabName;
    }

    public void RegisterSingleton<TConcrete>(TConcrete instance)
    {
        singletons[typeof(TConcrete)] = typeof(TConcrete);
        singletonInstances[typeof(TConcrete)] = instance;
    }

    public void RegisterSingletonReflectThree<TConcrete>(TConcrete instance)
    {
        singletons[typeof(TConcrete)] = typeof(TConcrete);
        singletonInstances[typeof(TConcrete)] = instance;
    }

    public void RegisterSingleton<TConcrete>(string prefab)
    {
        singletons[typeof(TConcrete)] = typeof(TConcrete);
        singletonsPrefabName[typeof(TConcrete)] = prefab;
    }

    public void RegisterTransient<TAbstract, TConcrete>(string prefab)
    {
        transients[typeof(TAbstract)] = typeof(TConcrete);
        singletonsPrefabName[typeof(TAbstract)] = prefab;
    }

    public void RegisterTransient<TAbstract, TConcrete>()
    {
        transients[typeof(TAbstract)] = typeof(TConcrete);
    }

    public T Resolve<T>() where T : class
    {
        return Resolve<T>(false);
    }

    public void UnRegisterSingleton<T>()
    {
        Type concreteType = null;
        if (singletons.TryGetValue(typeof(T), out concreteType))
        {
            if (singletonInstances.ContainsKey(typeof(T)))
            {
                singletonInstances.Remove(typeof(T));
            }
        }
    }

    public bool WasRegisterSingleton<T>()
    {
        return singletons.ContainsKey(typeof(T));
    }

    public bool WasResolved<T>()
    {
        return singletonInstances.ContainsKey(typeof(T));
    }



    /// <summary>
    /// A light weight resolve
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T ResolveUnSafe<T>()
    {
        return (T)singletonInstances[typeof(T)];
    }

    public T Resolve<T>(bool onlyExisting) where T : class
    {
        T result = default(T);
        Type concreteType = null;
        if (singletons.TryGetValue(typeof(T), out concreteType))
        {
            object r = null;
            if (!singletonInstances.TryGetValue(typeof(T), out r) && !onlyExisting)
            {
#if NETFX_CORE
					if (concreteType.GetTypeInfo().IsSubclassOf(typeof(MonoBehaviour))){
#else
                if (concreteType.IsSubclassOf(typeof(MonoBehaviour)))
                {
#endif
                    string prefabName = "";
                    if (singletonsPrefabName.TryGetValue(typeof(T), out prefabName))
                    {
                        var gameObj = Resources.Load<GameObject>(prefabName);
                        r = GameObject.Instantiate(gameObj).GetComponent<T>();
                    }
                    else
                    {
                        GameObject singletonGameObject = new GameObject();
                        r = singletonGameObject.AddComponent(concreteType);
                        singletonGameObject.name = typeof(T).ToString() + " (singleton)";
                    }
                }
                else
                    r = Activator.CreateInstance(concreteType);

                singletonInstances[typeof(T)] = r;

                IMultiSceneSingleton multi = r as IMultiSceneSingleton;
                if (multi != null)
                    multi.HandleNewSceneLoaded();
            }

            result = (T)r;
        }
        else if (transients.TryGetValue(typeof(T), out concreteType))
        {
            object r = Activator.CreateInstance(concreteType);
            result = (T)r;
        }

        return result;
    }
}
