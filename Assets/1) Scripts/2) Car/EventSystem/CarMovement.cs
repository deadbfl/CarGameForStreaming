using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : CarEventSystem
{
    private void OnEnable()
    {
        car.events.OnGasPedal += OnGasPedal;
        car.events.OnBreakPedal += OnBreakPedal;
        car.events.OnSteering += OnSteering;
    }

    private void OnDisable()
    {
        car.events.OnGasPedal -= OnGasPedal;
        car.events.OnBreakPedal -= OnBreakPedal;
        car.events.OnSteering -= OnSteering;
    }

    private void OnGasPedal(float value)
    {
        print("GasPedal: " + value);
    }
    
    private void OnBreakPedal(float value)
    {
        print("BreakPedal: " + value);
    }

    private void OnSteering(float value)
    {
        print("Steering: " + value);
    }
}
