using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Windows.Forms;
 
public class MenuItems : MonoBehaviour
{
    Logger logger = new Logger();

	// Use this for initialization
	void Start ()
    {

        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.DefaultExt = ".png";
        openFileDialog.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|All Files(*.*)|*.*";
        openFileDialog.InitialDirectory = UnityEngine.Application.dataPath;

        //openFileDialog.ShowDialog();

        string file = openFileDialog.FileName;
        if (file != null && file != "")
        {
            logger.Log(file);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Toggle(GameObject GO) 
    {
        GO.SetActive(!GO.activeSelf);
    }
}
