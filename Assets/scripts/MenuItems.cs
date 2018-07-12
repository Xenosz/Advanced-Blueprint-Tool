using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuItems : MonoBehaviour
{
    

	// Use this for initialization
	void Start ()
    {
        EditorUtility.DisplayDialog("Select File", "Test2", "OK");
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
