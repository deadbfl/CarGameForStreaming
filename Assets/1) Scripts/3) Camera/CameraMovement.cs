using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float clampAngle;
    private Vector3 refVelocity;

    private Vector2 rotateInput;

    private void FixedUpdate()
    {
        if (rotateInput != Vector2.zero)
        {
            Vector3 targetRotation = target.eulerAngles;

            targetRotation += Vector3.up * (rotateInput.x * rotateSpeed * Time.fixedDeltaTime);
            targetRotation -= Vector3.right * (rotateInput.y * rotateSpeed * Time.fixedDeltaTime);

            if (180 > targetRotation.x && targetRotation.x > clampAngle)
                targetRotation.x = clampAngle;
            else if (360 - clampAngle > targetRotation.x && targetRotation.x > 180)
                targetRotation.x = 360 - clampAngle;

            target.eulerAngles = targetRotation;
        }

        Vector3 currentTarget = target.position;

        currentTarget += target.right * offset.x;
        currentTarget += target.up * offset.y;
        currentTarget += target.forward * offset.z;

        transform.position = Vector3.SmoothDamp(transform.position, currentTarget, ref refVelocity, smoothTime);
        transform.LookAt(target);
    }

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
            rotateInput = context.ReadValue<Vector2>();
        else if (context.canceled)
            rotateInput = Vector2.zero;
    }
}