using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Gyroscope gyro;
    private bool gyroEnabled;
    private float x = 0.0f;
    private float y = 0.0f;

    GameObject camParent;

    // Use this for initialization
    void Start()
    {
        camParent = new GameObject("CamParent");
        camParent.transform.position = this.transform.position;
        this.transform.parent = camParent.transform;
        gyroEnabled = EnableGyro();
    }

    bool EnableGyro()
    {
        if ((SystemInfo.supportsGyroscope && Application.platform == RuntimePlatform.Android))
        {
            Debug.Log("Gyro enabled");
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!InputManager.instance.PauseGame)
        {
            if (gyroEnabled) updateScreen();
            else followMouse();
        }
    }

    void updateScreen()
    {
        camParent.transform.Rotate(0, -gyro.rotationRateUnbiased.y, 0);
        this.transform.Rotate(-gyro.rotationRateUnbiased.x, 0, 0);
    }

    void followMouse()
    {
        x += Input.GetAxis("Mouse X") * InputManager.instance.mouseXSpeedMod;
        y -= Input.GetAxis("Mouse Y") * InputManager.instance.mouseYSpeedMod;
        y = ClampAngle(y, -90, 90);
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        transform.rotation = rotation;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
