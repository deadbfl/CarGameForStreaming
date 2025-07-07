using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputScript : MonoBehaviour
{
    public void OnGasPedal(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        print(inputValue);
        if (context.performed)
        {
            print("Pressed GasPedal");
        }
        else if (context.canceled)
        {
            print("Canceled GasPedal");
        }
    }

    public void OnBrakePedal(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        print(inputValue);
        if (context.performed)
        {
            print("Pressed BrakePedal");
        }
        else if (context.canceled)
        {
            print("Canceled BrakePedal");
        }
    }

    public void OnSteering(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        print(inputValue);
    }
}