using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : CarEventSystem
{
    [SerializeField] private Transform COM; // Center Of Mass => Kutle Merkezi
    [SerializeField] private float downGravity;
    [SerializeField] private float enginePower;
    [SerializeField] private eAxleType carAxle;
    [SerializeField] private float wheelRadius = .35f;
    [SerializeField] private WheelInfo[] wheels;

    private Rigidbody rb;
    private float moveInput;
    private float steerInput;

    private List<int> powerWheel = new List<int>();

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();

        if (carAxle == eAxleType.Rear)
        {
            powerWheel.Add(2);
            powerWheel.Add(3);
        }
        else if (carAxle == eAxleType.Front)
        {
            powerWheel.Add(0);
            powerWheel.Add(1);
        }
        else if (carAxle == eAxleType.X4)
        {
            powerWheel.Add(0);
            powerWheel.Add(1);
            powerWheel.Add(2);
            powerWheel.Add(3);
        }
    }

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

    private void FixedUpdate()
    {
        bool isAllWheelFlying = true;
        rb.centerOfMass = COM.localPosition;
        for (int i = 0; i < wheels.Length; i++)
        {
            Transform wheelTransform = wheels[i].transform;
            Vector3 moveDir = wheelTransform.forward;
            Vector3 tireWorldVel = rb.GetPointVelocity(wheelTransform.position);
            Vector3 forceOnWheel = Vector3.zero;

            float compressionRate = 0;

            if (Physics.Raycast(wheelTransform.position, -wheelTransform.up, out RaycastHit hit, wheels[i].suspensionRestDistance))
            {
                Debug.DrawLine(wheelTransform.position, hit.point, Color.red);

                Vector3 springDir = wheelTransform.up;

                float velocity = Vector3.Dot(springDir, tireWorldVel);

                float compressionAmount = wheels[i].suspensionRestDistance - hit.distance;
                compressionRate = Mathf.Clamp01(compressionAmount / wheels[i].suspensionRestDistance);

                float springForce = wheels[i].springStrength * compressionAmount - velocity * wheels[i].springDamper;

                Vector3 springForceVec = springDir * springForce;

                forceOnWheel += springForceVec;

                isAllWheelFlying = false;
            }

            Vector3 moveForce = moveDir * (moveInput * enginePower);

            if (powerWheel.Contains(i)) // Ben harekete etki ediyorsam
                forceOnWheel += moveForce;

            rb.AddForceAtPosition(forceOnWheel, wheelTransform.position);

            // Visual

            Vector3 wheelOldPos = wheels[i].wheelModelTransform.localPosition;
            wheelOldPos.y = Mathf.Lerp(-.05f, .2f, compressionRate); // compressionRate => 0 = -0.05 => 1 = .2

            wheels[i].wheelModelTransform.localPosition = wheelOldPos;
        }

        if (isAllWheelFlying)
        {
            print("flying");
            // F = Mxa => F = 1000, a 1000/600 => 1.83
            float downForce = downGravity * rb.mass;
            rb.AddForceAtPosition(-COM.up * downForce, COM.position);
        }
    }

    private void OnGasPedal(float value)
    {
        moveInput = value;
    }

    private void OnBreakPedal(float value)
    {
        moveInput = -value;
    }

    private void OnSteering(float value)
    {
        steerInput = value;

        for (int i = 0; i < 2; i++)
        {
            wheels[i].transform.localEulerAngles = new Vector3(0, 30, 0) * steerInput;
            wheels[i].wheelModelTransform.localEulerAngles = new Vector3(0, 30, 0) * steerInput;
        }
    }
}

[Serializable]
public class WheelInfo
{
    public Transform wheelModelTransform;
    public Transform transform;
    public float suspensionRestDistance = 1;
    public float springDamper = 300;
    public float springStrength = 4000;
}

public enum eAxleType
{
    Rear,
    Front,
    X4,
}