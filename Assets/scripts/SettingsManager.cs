using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : Singleton<SettingsManager>
{
    protected SettingsManager() { }

    public Settings settings = new Settings();
    string path;
    string fileName = "Settings.json";
    string fullPath;

    void Awake()
    {
        path = Application.dataPath + "/Settings";
        fullPath = path + "/"  + fileName;

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    // Use this for initialization
    void Start()
    {
        settings.inDebugMode = true; // we maken wel een setting voor dit later.
        LoadJson();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadJson()
    {
        if (!File.Exists(fullPath))
            SaveJson();

        string rawJSON = File.ReadAllText(fullPath);
        settings = JsonUtility.FromJson<Settings>(rawJSON);
    }

    public void SaveJson()
    {
        string rawJSON = JsonUtility.ToJson(settings);
        //Debug.Log(fullPath);
        File.WriteAllText(fullPath,rawJSON);
    }

}

public class Logger
{
    public void Log(object ob)
    {
        if (SettingsManager.Instance.settings.inDebugMode) //Kan ik het uit zette in settings // voor latere setting
            UnityEngine.Debug.Log(ob); // debug log is mijn eigen dus ik moet aangeven dat ik unitys wil
    }
}
public class AdvancedLogger : Logger
{
    //can create custom logger here and have classes use this one instead without issues
}


public class Settings
{
    public bool inDebugMode;
    public float smoothAmount = 0.05f; //Default valuse here, if empty it will be null, AKA has to be set.
    public float mouseSensitivity = 0.1f;
    public float panSensitivity = 0.1f;
    public float zoomStep = 1f;
    public float defaultzoom = 1f;

    public string Game_Data;
    public string Mod_Data;
}




public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
