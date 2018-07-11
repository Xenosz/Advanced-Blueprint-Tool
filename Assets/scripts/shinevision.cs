using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shinevision : MonoBehaviour {
    public GameObject whatever;
    public GameObject GO;
    // Use this for initialization
    void Start ()
    {
        GO = Instantiate(whatever);
    }
	
	// Update is called once per frame
	void Update ()
    {
        GO.transform.position = transform.position;
        GO.transform.rotation = transform.rotation;
    }

    public void ResetPos()
    {
        Debug.Log("RESET");
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
