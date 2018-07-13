using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    Vector3 beginpos;
    Vector3 collapsedpos;
    Vector3 destinationpos;
    public RectTransform panelButton;
    public float smoothing = 0.05f;
    bool collapsed = false;

    // Use this for initialization
    void Start()
    {
        beginpos = GetComponent<RectTransform>().position;
        collapsedpos = new Vector3(beginpos.x - (panelButton.position.x - (panelButton.sizeDelta.x / 4)), GetComponent<RectTransform>().position.y, GetComponent<RectTransform>().position.z);
        GetComponent<RectTransform>().position = collapsedpos;
        destinationpos = beginpos;
    }
	// Update is called once per frame
	void Update ()
    {
        if(destinationpos != GetComponent<RectTransform>().position)
            GetComponent<RectTransform>().position = Vector3.Lerp(GetComponent<RectTransform>().position, destinationpos, smoothing);

    }

    public void ToggleActive()
    {

    }
    
    public void ToggleSize()
    {//width: 600 
        if(collapsed)
        {
            destinationpos = beginpos;

            //GetComponent<RectTransform>().position = beginpos;
            collapsed = false;
        }
        else 
        {
            destinationpos = collapsedpos;

            //GetComponent<RectTransform>().position = new Vector2(beginpos.x - (panelButton.position.x - (panelButton.sizeDelta.x / 4)), GetComponent<RectTransform>().position.y ); 
            collapsed = true;
        }
    }
}
