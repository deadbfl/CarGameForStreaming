using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInput : CarEventSystem
{
    public void OnGasPedal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            car.events.GasPedal(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            car.events.GasPedal(0);
        }
    }

    public void OnBrakePedal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            car.events.BreakPedal(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            car.events.BreakPedal(0);
        }
    }

    public void OnClutchPedal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            car.events.ClutchPedal(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            car.events.ClutchPedal(0);
        }
    }

    public void OnGearUp(InputAction.CallbackContext context)
    {
        if (context.performed)
            car.events.GearUp();
    }

    public void OnGearDown(InputAction.CallbackContext context)
    {
        if(context.performed)
            car.events.GearDown();
    }

    public void OnSteering(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            car.events.Steering(context.ReadValue<float>());
        }
        else if (context.canceled)
        {
            car.events.Steering(0);
        }
    }
}
