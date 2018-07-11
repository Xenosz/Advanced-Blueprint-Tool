using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    public Transform CreationTransformation;
    public Camera renderCamera;
    [Space(25)]
    [Header("Settings")]
    public float zoomStep = 1;
    public float mouseSensitivity = 0.01f;
    public bool onHoverRenderView = false;
    public float ObjectDistance;
    Vector3 camStartPos;

    Vector3 prevMousePos;
    Vector3 curMousePos;

    Vector3 CamPos;
    Vector3 Center;
    

    Vector3 panning;

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Over!");
        onHoverRenderView = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        onHoverRenderView = false;
    }

    // Use this for initialization
    void Start ()
	{
        ObjectDistance = renderCamera.transform.position.z;
        camStartPos = renderCamera.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!onHoverRenderView) return;

        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition * mouseSensitivity;
        }

        if (Input.GetMouseButton(0))
        {
            curMousePos = Input.mousePosition * mouseSensitivity;
            CamPos = prevMousePos - curMousePos;

        }
        if (onHoverRenderView)
        {
            UpdateRender();
        }
        prevMousePos = curMousePos;
    }

    void UpdateRender()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // Scroll forward
        {
            Debug.Log("Scroll > 0");
            ObjectDistance += zoomStep;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // Scroll backwards
        {
            Debug.Log("Scroll < 0");
            ObjectDistance -= zoomStep;
        }

        if (Input.GetMouseButtonDown(2))
        {
            prevMousePos = Input.mousePosition * mouseSensitivity;
        }

        if (Input.GetMouseButton(2))//middle mouse click pressed
        {
            curMousePos = Input.mousePosition * mouseSensitivity;
            panning += prevMousePos - curMousePos;

            renderCamera.transform.position = panning;
        }
        

        //renderCamera.transform.Translate(moveData);
        //CreationTransformation.transform.rotation = Quaternion.Euler(new Vector3(0, CamPos.x, 0) * 10);


        renderCamera.transform.eulerAngles += new Vector3(-CamPos.y, CamPos.x, 0);

        //moveData = Vector3.zero;


        //renderCamera.transform.LookAt(pivot.transform.position);

        if (ObjectDistance != renderCamera.transform.position.z) // zoom
        {
            Debug.Log("zoom");
            renderCamera.gameObject.transform.position = new Vector3(renderCamera.transform.position.x, renderCamera.transform.position.y, ObjectDistance);
        }
    }

    public void ResetPos()
    {
        Debug.Log("RESET");
        transform.rotation = Quaternion.Euler(Vector3.zero);
        prevMousePos = Vector3.zero;
        curMousePos = Vector3.zero;
        CamPos = Vector3.zero;
        ObjectDistance = camStartPos.z;
        renderCamera.transform.position = camStartPos;
        UpdateRender();
    }
}