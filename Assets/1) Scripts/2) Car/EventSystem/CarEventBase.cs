using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarEventBase : MonoBehaviour
{
    // Arabam ile alakali eventleri tutacak
    public CarEvents events;
}

public struct CarEvents
{
    public event Action<float> OnGasPedal; // Floatin amaci oyuncudan alinan inputu islemek
    public void GasPedal(float value) => OnGasPedal?.Invoke(value); // Oyuncunun aracini hareket ettirecek komut
    
    public event Action<float> OnBreakPedal;
    public void BreakPedal(float value) => OnBreakPedal?.Invoke(value); // Cagrildigi zaman
    
    public event Action<float> OnClutchPedal;
    public void ClutchPedal(float value) => OnClutchPedal?.Invoke(value);
    
    public event Action<float> OnSteering;
    public void Steering(float value) => OnSteering?.Invoke(value);
}
