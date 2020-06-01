using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    private float currentZoom = 10f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float pitchSpeed = 120f;
    public float yawSpeed = 120f;
    private float pitch = 0f;

    private float currentPitch = 0f;
    private float currentYaw = 0f;

    void Update() 
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        currentPitch -= Input.GetAxis("Vertical") * pitchSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, -35,1);
        //Debug.Log("Current Pitch: " +currentPitch);
        //Debug.Log("Current Yaw: " +currentYaw);
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.right, currentPitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);
        
    }
}
