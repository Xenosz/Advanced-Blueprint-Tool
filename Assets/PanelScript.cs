using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    Vector2 beginpos;
    Vector2 collapseSize;
    public RectTransform panelButton;
    bool collapsed = false;

    // Use this for initialization
    void Start()
    {
        beginpos = GetComponent<RectTransform>().position;
        collapseSize = panelButton.position;
    }
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void ToggleActive()
    {

    }
    
    public void ToggleSize()
    {//width: 600 
        if(collapsed)
        {
            GetComponent<RectTransform>().position = beginpos;
            collapsed = false;
        }
        else 
        {
            GetComponent<RectTransform>().position = new Vector2(beginpos.x - (panelButton.position.x - (panelButton.sizeDelta.x / 4) /* w8, why is het / 4 we can try i guess? panelButton.sizeDelta.x  is maybe hetzelfde? Tadaaaaaaaaaaaaaaa*/), GetComponent<RectTransform>().position.y ); 
            collapsed = true;
        }
    }
}
