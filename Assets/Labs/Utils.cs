using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
//using ICSharpCode.SharpZipLib.Zip;
using UnityEngine;


public static class Base64Url
{
    public static string Encode(byte[] input)
    {
        return Convert.ToBase64String(input).Split('=')[0].Replace('+', '-').Replace('/', '_');
    }

    public static byte[] Decode(string input)
    {
        string s = input.Replace('-', '+').Replace('_', '/');
        switch (s.Length % 4)
        {
            case 0:
                return Convert.FromBase64String(s);
            case 2:
                s += "==";
                goto case 0;
            case 3:
                s += "=";
                goto case 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), "Illegal base64url string!");
        }
    }
}

public class Compact
{
    public static string Serialize(params byte[][] parts)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte[] part in parts)
            stringBuilder.Append(Base64Url.Encode(part)).Append(".");
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }

    public static byte[][] Parse(string token)
    {
        string[] strArray = token.Split('.');
        byte[][] numArray = new byte[strArray.Length][];
        for (int index = 0; index < strArray.Length; ++index)
            numArray[index] = Base64Url.Decode(strArray[index]);
        return numArray;
    }
}

public static class Utils
{
    public static IEnumerable<T> GetAllClass<T>()
    {
        var type = typeof(T);
        return AppDomain.CurrentDomain.GetAssemblies().Where(s => s.IsDynamic == false)
          .SelectMany(s => s.GetTypes())
          .Where(p => type.IsAssignableFrom(p) && p.IsInterface == false && p.IsAbstract == false)
          .Select(q => (T)Activator.CreateInstance(q));
    }

    public static Type GetType(string assemblyName, string typename)
    {
        var assembly = Assembly.Load(assemblyName);
        return assembly.GetType(typename);
    }

    public static string GetStreamingAssetsPath()
    {
        string path = Application.dataPath + "/StreamingAssets";

#if !UNITY_EDITOR && UNITY_ANDROID
 path = "jar:file://" + Application.dataPath + "!/assets/";

#elif !UNITY_EDITOR && UNITY_IOS
 path = Application.dataPath + "/Raw";
#endif
        return path;
    }

    public static byte[] GenerateHMACMD5(byte[] messageBytes, byte[] publicKey)
    {
        HMACMD5 hmac5 = new HMACMD5(publicKey);

        byte[] hashmessage = hmac5.ComputeHash(messageBytes);

        return hashmessage;
    }

    /// <summary>
    /// Get all derived types from a type<c>T</c>.
    /// </summary>
    /// <typeparam name="T">Type to get derived classes.</typeparam>
    public static IEnumerable<Type> GetAllTypes<T>()
    {
        var type = typeof(T);
        return AppDomain.CurrentDomain.GetAssemblies().Where(s => s.IsDynamic == false)
          .SelectMany(s => s.GetTypes())
          .Where(p => type.IsAssignableFrom(p) && p.IsInterface == false && p.IsAbstract == false);
    }

    public static IEnumerable<Type> GetAllTypes(Type T)
    {
        var type = T;
        return AppDomain.CurrentDomain.GetAssemblies().Where(s => s.IsDynamic == false)
          .SelectMany(s => s.GetTypes())
          .Where(p => type.IsAssignableFrom(p) && p.IsInterface == false && p.IsAbstract == false);
    }

    public static IEnumerable<Type> GetAllTypesFromAtribute(Type attributeType)
    {
        return AppDomain.CurrentDomain.GetAssemblies().Where(s => s.IsDynamic == false)
          .SelectMany(s => s.GetTypes())
          .Where(p => p.GetType().GetCustomAttributes(attributeType) != null);
    }

    public static string GetTokenPayLoad(string token)
    {
        byte[][] numArray = Compact.Parse(token);
        if (numArray.Length > 3)
            throw new Exception(
              "Getting payload for encrypted tokens is not supported.");
        return Encoding.UTF8.GetString(numArray[1]);
    }

    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static T ToEnum<T>(this string value, bool showWarning = false)
    {
        try
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        catch (Exception)
        {
            if (showWarning)
                Debug.Log($"[WARNING] Failed to part string \"{value}\" to enum {typeof(T)}");
            return default(T);
        }
    }

    public static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>
      (Dictionary<TKey, TValue> original) where TValue : ICloneable
    {
        Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
          original.Comparer);
        foreach (KeyValuePair<TKey, TValue> entry in original)
        {
            ret.Add(entry.Key, (TValue)entry.Value.Clone());
        }

        return ret;
    }

    public static T CreatePrefab<T>(string path, Transform parent) where T : Component
    {
        GameObject prefab = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        if (prefab == null)
        {
            Debug.LogError("Prefab Instantiation Error. Prefab with type: " + typeof(T) + " and path: " + path +
                           " could not be instantiated.");
            return null;
        }

        if (parent != null)
            prefab.transform.SetParent(parent, false);

        return prefab.GetComponent<T>();
    }

    /// <summary>
    /// Loads the scriptable object.
    /// </summary>
    /// <returns>The scriptable object.</returns>
    /// <param name="path">Path.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T LoadScriptableObject<T>(string path) where T : ScriptableObject
    {
        return Resources.Load<T>(path);
    }

    public static string GetZipEntryString(byte[] buffers, string entryName)
    {
        //var rawData = lzip.entry2Buffer(null, entryName, buffers);
        //return Encoding.UTF8.GetString(rawData);
        return "";
    }

    public static Dictionary<string, string> GetAllDataFilesFromZipBuffer(byte[] buffers)
    {
        //ZipFile zf = new ZipFile(new MemoryStream(buffers));
        //Dictionary<string, string> extractData = new Dictionary<string, string>();

        //using (ZipInputStream s = new ZipInputStream(new MemoryStream(buffers))) {
        //  ZipEntry nextEntry;
        //  while ((nextEntry = s.GetNextEntry()) != null) {
        //    using (StreamReader sr = new StreamReader(zf.GetInputStream(nextEntry))) {
        //      var data = sr.ReadToEnd();
        //      extractData.Add(nextEntry.Name, data);
        //    }
        //  }
        //}

        //return extractData;
        return null;
    }

    public static bool GetWebFileSize(string url, out int size)
    {
        System.Net.WebRequest req = System.Net.HttpWebRequest.Create(url);
        req.Method = "HEAD";
        try
        {
            using (System.Net.WebResponse wr = req.GetResponse())
            {
                int length;
                if (int.TryParse(wr.Headers.Get("Content-Length"), out length))
                {
                    size = length;
                    return true;
                }
            }
        }
        catch (System.Exception e)
        {
        }

        size = 0;
        return false;
    }

    public static string GetPath(this Transform current)
    {
        if (current.parent == null)
            return current.name;
        return current.parent.GetPath() + "/" + current.name;
    }

    public static string CountDownTimeString(long time)
    {
        string result = string.Empty;
        TimeSpan timeSpan = TimeSpan.FromSeconds(1d * time / 1000);

        if (timeSpan.Hours == 0)
        {
            if (timeSpan.Minutes == 0)
            {
                result = timeSpan.Seconds > 0 ? "01MIN" : "00MIN";
            }
            else
            {
                result = timeSpan.Minutes + (timeSpan.Minutes > 1 ? "MINS" : "MIN");
            }
        }
        else
        {
            result = string.Format("{0:D2}H {1:D2}MIN", timeSpan.Hours, timeSpan.Minutes);
        }

        return result;
    }

    public static string TimeStringFromSeconds(long time)
    {
        string result = string.Empty;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        if (timeSpan.Hours == 0)
        {
            if (timeSpan.Minutes == 0)
            {
                result = timeSpan.Seconds > 0 ? "01MIN" : "00MIN";
            }
            else
            {
                result = timeSpan.Minutes + (timeSpan.Minutes > 1 ? "MINS" : "MIN");
            }
        }
        else
        {
            result = string.Format("{0:D2}H {1:D2}MIN", timeSpan.Hours, timeSpan.Minutes);
        }

        return result;
    }

    public static T[] ToArray<T>(this string s, Func<string, T> parser, char delim = ',', bool debug = false)
    {
        var temp = s.Split(delim);
        var array = new T[temp.Length];
        for (var i = 0; i < temp.Length; i++)
        {
            try
            {
                array[i] = parser(temp[i]);
            }
            catch (Exception e)
            {
                if (debug)
                    Debug.Log(e);
                array[i] = default(T);
            }
        }

        return array;
    }

    public static List<T> ToList<T>(this string s, Func<string, T> parser, char delim = ',', bool debug = false)
    {
        var temp = s.Split(delim);
        var list = new List<T>();
        foreach (var t in temp)
        {
            try
            {
                list.Add(parser(t));
            }
            catch (Exception e)
            {
                if (debug)
                    Debug.Log(e);
                list.Add(default(T));
            }
        }

        return list;
    }

    public static int[] ToArrayInt(this string s, char delim = ',', bool debug = false)
    {
        return s.ToArray(int.Parse, delim, debug);
    }

    public static List<int> ToListInt(this string s, char delim = ',', bool debug = false)
    {
        return s.ToList(int.Parse, delim, debug);
    }

    public static float[] ToArrayFloat(this string s, char delim = ',', bool debug = false)
    {
        return s.ToArray(float.Parse, delim, debug);
    }

    public static List<float> ToListFloat(this string s, char delim = ',', bool debug = false)
    {
        return s.ToList(float.Parse, delim, debug);
    }

    public static long[] ToArrayLong(this string s, char delim = ',', bool debug = false)
    {
        return s.ToArray(long.Parse, delim, debug);
    }

    public static List<long> ToListLong(this string s, char delim = ',', bool debug = false)
    {
        return s.ToList(long.Parse, delim, debug);
    }

    public static double[] ToArrayDouble(this string s, char delim = ',', bool debug = false)
    {
        return s.ToArray(double.Parse, delim, debug);
    }

    public static List<double> ToListDouble(this string s, char delim = ',', bool debug = false)
    {
        return s.ToList(double.Parse, delim, debug);
    }

    public static T[] ToArrayEnum<T>(this string s, char delim = ',', bool debug = false)
      where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        return s.ToArray(e => (T)Enum.Parse(typeof(T), e), delim, debug);
    }

    public static List<T> ToListEnum<T>(this string s, char delim = ',', bool debug = false)
      where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        return s.ToList(e => (T)Enum.Parse(typeof(T), e), delim, debug);
    }
}
