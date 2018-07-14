using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Windows.Forms;

public class ResourceManager : Singleton<ResourceManager>
{
    private string User_;

    public long UserID { get; private set; }

    public static Dictionary<string, Sprite> blueprints = new Dictionary<string, Sprite>();//path, img
    public static Dictionary<string, Blockobject> blocks = new Dictionary<string, Blockobject>();//uuid, block

    //public static Dictionary<string, string> usedmods = new Dictionary<string, string>();//id, name
    //public static List<Notification> Notifications = new List<Notification>();

    // Use this for initialization
    void Start ()
    {
        findPaths();


	}

    public void findPaths()
    {
        #region get User_ dir, based on last used directory, used for appdata mods and blueprints.
        string userdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Axolot Games\\Scrap Mechanic\\User";
        DateTime lasthigh = new DateTime(1900, 1, 1);
        string dir = "";
        foreach (string subdir in Directory.GetDirectories(userdir)) //get user_numbers folder that is last used
        {
            DirectoryInfo fi1 = new DirectoryInfo(subdir + @"\blueprints");
            DateTime created = fi1.LastWriteTime;
            fi1 = new DirectoryInfo(subdir);
            if (created > lasthigh)
            {
                dir = subdir;
                lasthigh = created;
            }
        }
        User_ = dir;
        #endregion

        SettingsManager.Instance.LoadJson(); //make sure settings are loaded




    }


    // Update is called once per frame
    void Update () {
		
	}
}
