using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]

    public Camera renderCamera;
    [Space(25)]
    [Header("Settings")]
    public float zoomStep;
    public float mouseSensitivity;
    public float panSensitivity;
    public float smoothing;
    public bool onHoverRenderView = false;
    public float defaultzoom;
    Vector3 camStartPos;

    Vector3 prevMousePos;
    Vector3 curMousePos;

    Vector3 CamMove = Vector3.zero;
    Vector3 PanMove = Vector3.zero;
    
    Vector3 center = Vector3.zero;

    Logger logger = new Logger();

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        logger.Log("Over!");
        onHoverRenderView = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        logger.Log("Exit");
        onHoverRenderView = false;
    }

    // Use this for initialization
    void Start ()
	{
        Settings settings = SettingsManager.Instance.settings;

        if(!settings.inDebugMode)
        {
            smoothing = settings.smoothAmount;
            mouseSensitivity = settings.mouseSensitivity;
            panSensitivity = settings.panSensitivity;
            zoomStep = settings.zoomStep;
            defaultzoom = settings.defaultzoom;
        }
        camStartPos = renderCamera.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (onHoverRenderView) // only register input when hovering over, do allow render when not over tho
        {
            if (Input.GetMouseButtonDown(0))
            {
                prevMousePos = Input.mousePosition * mouseSensitivity;
            }
            if (Input.GetMouseButtonDown(2))
            {
                prevMousePos = Input.mousePosition * mouseSensitivity;
            }

            if (Input.GetMouseButton(0))
            {
                curMousePos = Input.mousePosition * mouseSensitivity;
                CamMove += (prevMousePos - curMousePos)/2; 
            }
            if (Input.GetMouseButton(2))//middle mouse click pressed
            {
                curMousePos = Input.mousePosition * mouseSensitivity;
                PanMove += (prevMousePos - curMousePos)/2;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0) // Scroll forward
            {
                logger.Log("Scroll > 0");
                defaultzoom = zoomStep;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // Scroll backwards
            {
                logger.Log("Scroll < 0");
                defaultzoom = -zoomStep;
            }
            prevMousePos = curMousePos;
        }
        
        UpdateRender();

    }

    void UpdateRender()
    {
        Vector3 lookdir = renderCamera.transform.TransformDirection(new Vector3(0, 0, 1));

        {
            //pan:
            float x = renderCamera.transform.rotation.eulerAngles.x;
            if (Vector3.Cross(lookdir, new Vector3(0, 1, 0)).sqrMagnitude > 0.006 ||
                ((x < 100 &&PanMove.y > 0) || (x > 250 && PanMove.y < 0)))
                center += renderCamera.transform.TransformDirection(PanMove) * panSensitivity;
            renderCamera.transform.LookAt(center);

            //zoom:
            renderCamera.transform.position += lookdir * defaultzoom;
            defaultzoom = 0;

            //rotate camera:
            renderCamera.transform.RotateAround(center, new Vector3(0, 1, 0), -CamMove.x);

            //90° top and bottom limit:
            x = renderCamera.transform.rotation.eulerAngles.x;
            if (Vector3.Cross(lookdir, new Vector3(0, 1, 0)).sqrMagnitude>0.006 || //Extremely tiny optimized code with little math coded by a profesional.
                //(Vector3.Cross(lookdir, new Vector3(0, 1, 0)).sqrMagnitude > 0.0005 && Mathf.Abs(CamMove.y)<1) || // allow to get closer, tho only at slower pace
                ((x<100 && CamMove.y<0 ) || (x >250 && CamMove.y > 0))  )
                renderCamera.transform.RotateAround(center, -renderCamera.transform.right, -CamMove.y);
        }
        //sideways rotation lock (z):
        renderCamera.transform.rotation = Quaternion.Euler(renderCamera.transform.rotation.eulerAngles.x, renderCamera.transform.rotation.eulerAngles.y, 0);
        //smoothing:
        PanMove = Vector3.Lerp(PanMove, Vector3.zero, smoothing);
        CamMove = Vector3.Lerp(CamMove, Vector3.zero, smoothing);
    }
    private Vector3 eulervector(Vector3 euler)
    {
        float elevation = Mathf.Deg2Rad * (euler.x);
        float heading = Mathf.Deg2Rad * (euler.y);
        return new Vector3(Mathf.Cos(elevation) * Mathf.Sin(heading), Mathf.Sin(elevation), Mathf.Cos(elevation) * Mathf.Cos(heading));
    }
    public void ResetPos()
    {
        logger.Log("Resetting the view port.");
        transform.rotation = Quaternion.Euler(Vector3.zero);
        prevMousePos = Vector3.zero;
        curMousePos = Vector3.zero;
        CamMove = Vector3.zero;
        PanMove = Vector3.zero;
        defaultzoom = 0;
        center = Vector3.zero;
        renderCamera.transform.position = camStartPos;
        UpdateRender();
    }
}