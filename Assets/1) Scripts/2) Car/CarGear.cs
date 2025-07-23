using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGear : CarEventSystem
{
    [SerializeField] private eGears currentGearType;
    [Header("Max Values")]
    [SerializeField] private int maxGearInForward;
    [SerializeField] private int maxGearInReverse;
    [Header("Silinecek")]
    [SerializeField] private int currentGearValue = 0;

    private void OnEnable()
    {
        car.events.OnGearUp += IncreaseGear;
        car.events.OnGearDown += DecreaseGear;
    }

    private void OnDisable()
    {
        car.events.OnGearUp -= IncreaseGear;
        car.events.OnGearDown -= DecreaseGear;
    }

    private void IncreaseGear() => SetCurrentGear(currentGearValue + 1);
    private void DecreaseGear() => SetCurrentGear(currentGearValue - 1);

    private void SetCurrentGear(int newGear)
    {
        currentGearValue = Mathf.Clamp(newGear, maxGearInReverse, maxGearInForward);

        if (currentGearValue > 0)
            currentGearType = eGears.Drive;
        else if (currentGearValue < 0)
            currentGearType = eGears.Reverse;
        else
            currentGearType = eGears.Neutral;
        
        car.events.GearChanged(currentGearValue);
    }
}

public enum eGears
{
    Neutral = 0,
    Park = 1,
    Reverse = 2,
    Drive = 3,
}