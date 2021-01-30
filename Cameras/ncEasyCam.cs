using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ncEasyCam : MonoBehaviour
{

    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float zoomSpeed = 2f;
    public float dragSpeed = 2f;

    public bool bSetOrhoView = false;
    public bool bSetPerspectiveView = true;

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {

        if (bSetPerspectiveView)
        {
            //Look around with Right Mouse
            if (Input.GetMouseButton(1))
            {
                yaw += lookSpeedH * Input.GetAxis("Mouse X");
                pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

                transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            }

            //drag camera around with Middle Mouse
            if (Input.GetMouseButton(0))
            {
                transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
            }

            //Zoom in and out with Mouse Wheel
            transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
        }
        else
        {
            //drag camera around with Middle Mouse
            if (Input.GetMouseButton(0))
            {
                transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
            }

            Camera cam = gameObject.GetComponent<Camera>();
            cam.orthographicSize = cam.orthographicSize + (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
        }
        if (Input.GetKeyDown("f"))
        {

            bSetOrhoView = true;
            bSetPerspectiveView = false;
            Camera cam = gameObject.GetComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 2;
            transform.position = new Vector3(0, 1, -1);
            transform.rotation = Quaternion.identity;

        }

        if (Input.GetKeyDown("s"))
        {
            bSetOrhoView = true;
            bSetPerspectiveView = false;
            Camera cam = gameObject.GetComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 5;
            transform.position = new Vector3(-1, 1, 0);
            transform.rotation = Quaternion.Euler(0, 90, 0);

        }


        if (Input.GetKeyDown("t"))
        {
            bSetOrhoView = true;
            bSetPerspectiveView = false;
            Camera cam = gameObject.GetComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 5;
            transform.position = new Vector3(0, 1, 0);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        if (Input.GetKeyDown("p"))
        {

            bSetOrhoView = false;
            bSetPerspectiveView = true;
            Camera cam = gameObject.GetComponent<Camera>();
            cam.orthographic = false;
            transform.position = new Vector3(0, 1, 0);
            transform.rotation = Quaternion.identity;


        }

    }
}