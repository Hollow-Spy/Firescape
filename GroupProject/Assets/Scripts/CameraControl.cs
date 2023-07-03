using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

  

    void LateUpdate()
    {
        // Define a target position behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -30));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}