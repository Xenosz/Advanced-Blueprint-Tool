using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    public static Settings settings = new Settings();
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
        SaveJson();
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

    public class Debug //<---
    {
        public static void Log(object ob)
        {
            if (SettingsManager.settings.inDebugMode) //Kan ik het uit zette in settings // voor latere setting
                UnityEngine.Debug.Log(ob); // debug log is mijn eigen dus ik moet aangeven dat ik unitys wil
        }
    }
}

public class Settings
{
    public bool inDebugMode;
    public float smoothAmount = 0.05f; //Default valuse here, if empty it will be null, AKA has to be set.
    public float mouseSensitivity = 0.1f;
    public float panSensitivity = 0.1f;
    public float zoomStep = 1f;
    public float defaultzoom = 1f;
    // Meer settings hier ofzo.
}
