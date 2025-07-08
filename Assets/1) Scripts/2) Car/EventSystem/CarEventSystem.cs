using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEventSystem : MonoBehaviour
{
    protected CarEventBase car;
    // Burasinin amaci CarEventBase erismek
    protected virtual void Awake()
    {
        car = GetComponent<CarEventBase>();
    }
}
