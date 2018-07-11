using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    public Transform pivot;
    public Camera renderCamera;
    [Space(25)]
    [Header("Settings")]
    public float zoomStep = 1;
    public float mouseSensitivity = 0.01f;
    public bool onHoverRenderView = false;
    public float objectZ;
    Vector3 camStartPos;

    Vector3 prevMousePos;
    Vector3 curMousePos;

    Vector3 moveData;

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
        objectZ = renderCamera.transform.position.z;
        camStartPos = renderCamera.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
	{

        if (Input.GetMouseButtonDown(0))
        {
            
            prevMousePos = Input.mousePosition * mouseSensitivity;
        }

        if (Input.GetMouseButton(0))
        {
            curMousePos = Input.mousePosition * mouseSensitivity;
            moveData += prevMousePos - curMousePos;
            
        }
        if (onHoverRenderView)
        {
            UpdateRender();
        }
    }

    void UpdateRender()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // Scroll forward
        {
            Debug.Log("Scroll > 0");
            objectZ += zoomStep;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // Scroll backwards
        {
            Debug.Log("Scroll < 0");
            objectZ -= zoomStep;
        }

        if (Input.GetMouseButton(2))
        {
            renderCamera.transform.position = new Vector3(
            (Input.mousePosition.x / 100) - 4,
            (Input.mousePosition.y / 100) - 3,
            Input.mousePosition.z
            );
        }


        /*renderCamera.transform.position = new Vector3(
            (Input.mousePosition.x / 100) - 4,
            (Input.mousePosition.y / 100) - 3,
            Input.mousePosition.z
            );*/

        //renderCamera.transform.LookAt(pivot.transform);
        //renderCamera.transform.Translate(moveData);
        pivot.transform.rotation = Quaternion.Euler(new Vector3(moveData.y * -1, moveData.x, moveData.z * -1) * 10);
        //moveData = Vector3.zero;

        //renderCamera.transform.LookAt(pivot.transform.position);

        if (objectZ != renderCamera.transform.position.z)
        {
            Debug.Log("SET");
            renderCamera.gameObject.transform.position = new Vector3(renderCamera.transform.position.x, renderCamera.transform.position.y, objectZ);
        }
    }

    public void ResetPos()
    {
        Debug.Log("RESET");
        transform.rotation = Quaternion.Euler(Vector3.zero);
        prevMousePos = Vector3.zero;
        curMousePos = Vector3.zero;
        moveData = Vector3.zero;
        objectZ = camStartPos.z;
        renderCamera.transform.position = camStartPos;
        UpdateRender();
    }
}