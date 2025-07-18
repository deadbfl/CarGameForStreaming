using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;

    private Vector3 refVelocity;

    private void FixedUpdate()
    {
        Vector3 currentTarget = target.position;
        
        currentTarget += target.right * offset.x;
        currentTarget += target.up * offset.y;
        currentTarget += target.forward * offset.z;

        transform.position = Vector3.SmoothDamp(transform.position, currentTarget, ref refVelocity, smoothTime);
        transform.LookAt(target);
    }
}