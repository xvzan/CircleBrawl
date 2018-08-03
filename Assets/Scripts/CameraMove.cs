using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 camspeed = new Vector3(0, 0, 0);
    Vector3 camspeed2 = new Vector3(0, 0, 0);
    Vector3 oriplace;
    float speed2 = 7.5f;
    int edgewidth = 1;

    // Use this for initialization
    void Start ()
    {
        oriplace = transform.position;
	}

    // Update is called once per frame
    private void Update()
    {
        if (Input.mousePosition.x < Screen.width - edgewidth && Input.mousePosition.x > edgewidth && Input.mousePosition.y < Screen.height - edgewidth && Input.mousePosition.y > edgewidth)
        {
            camspeed = new Vector3(0, 0, 0);
            camspeed2 = new Vector3(0, 0, 0);
        }
        if (Input.mousePosition.x >= Screen.width - edgewidth)
        {
            camspeed.x = Mathf.Max(0, Input.GetAxis("Mouse X"));
            camspeed2.x = speed2 * Time.deltaTime;
        }
        if (Input.mousePosition.x <= edgewidth)
        {
            camspeed.x = Mathf.Min(0, Input.GetAxis("Mouse X"));
            camspeed2.x = -speed2 * Time.deltaTime;
        }
        if (Input.mousePosition.y >= Screen.height - edgewidth)
        {
            camspeed.y = Mathf.Max(0, Input.GetAxis("Mouse Y"));
            camspeed2.y = speed2 * Time.deltaTime;
        }
        if (Input.mousePosition.y <= edgewidth)
        {
            camspeed.y = Mathf.Min(0, Input.GetAxis("Mouse Y"));
            camspeed2.y = -speed2 * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        Camera.main.orthographicSize -= Input.GetAxis("Mouse Scrollwheel");
        transform.position += camspeed + camspeed2;
        if (Input.GetButtonDown("ResetCam"))
        {
            transform.position = oriplace;
        }
        if (Camera.main.orthographicSize < 1)
            Camera.main.orthographicSize = 1;
    }
}
