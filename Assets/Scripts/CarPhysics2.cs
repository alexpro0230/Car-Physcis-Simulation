using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics2 : MonoBehaviour {
    [Header("Wheel Arrays")]
    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] backWheels = new WheelCollider[2];

    public WheelCollider[] allWheels = new WheelCollider[4];

    public GameObject[] wheelGraphics = new GameObject[4];

    [Header("\nDriving Simulation Variables")]
    public float torque;
    public float BreakForce;

    private bool isBraking;
    private float breakForce;
    
    [Header("\nComponents")]
    public Rigidbody rb;

    private void Start() {
        for (int i = 0; i < 4; i++) {
            allWheels[i] = transform.Find("wheel colliders").GetChild(i).GetComponent<WheelCollider>();
        }

        isBraking = false;
    }
    private void FixedUpdate() {
        SimulateWheelPhysics();
    }

    private void Update() {
        GetInput();
        applyBrake();
        UpdateWheelGraphics();
    }


    #region simulation
    
    
    void applyBrake() {
        if (isBraking) {
            breakForce = BreakForce;
        } else {
            breakForce = 0;
        }

        foreach (WheelCollider obj in allWheels) {
            obj.brakeTorque = breakForce;
        }
    }

    void SimulateWheelPhysics() {
        foreach (WheelCollider obj in backWheels) {
            obj.motorTorque = torque * Input.GetAxis("Vertical");
        }

        foreach (WheelCollider obj in frontWheels) {
            obj.steerAngle = 45f * Input.GetAxis("Horizontal");
        }
    }


    #endregion

    #region input
    
    void GetInput() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            isBraking = true;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            isBraking = false;
        }
    }
    
    #endregion

    #region graphic


    void UpdateWheelGraphics() {
        for (int i = 0; i < wheelGraphics.Length; i++) {
            Quaternion rotation;
            Vector3 pos;

            allWheels[i].GetWorldPose(out pos, out rotation);

            wheelGraphics[i].transform.position = pos;
            wheelGraphics[i].transform.rotation = rotation;
        }
    }

    #endregion
}
