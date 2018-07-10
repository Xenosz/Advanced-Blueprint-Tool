using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuContentBehaviourScript : MonoBehaviour {
    public GameObject ItemHolder;
    public Sprite sprite;

	// Use this for initialization
	void Start ()
    {
        GameObject GO = Instantiate(ItemHolder);
        GO.transform.SetParent(transform);
        GO.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        GO.transform.GetChild(1).GetComponent<Text>().text = "shinevision <3";

        GameObject GO1 = Instantiate(ItemHolder);
        GO1.transform.SetParent(transform);
        GO1.transform.GetChild(1).GetComponent<Text>().text = "maar wat nu met een langere text";

        List<GameObject> buttons = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
