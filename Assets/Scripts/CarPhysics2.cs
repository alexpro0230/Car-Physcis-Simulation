using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarPhysics2 : MonoBehaviour
{

    #region arrays

    [Header("Wheel Arrays")]
    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] backWheels = new WheelCollider[2];
    public WheelCollider[] allWheels = new WheelCollider[4];

    public GameObject[] wheelGraphics = new GameObject[4];

    #endregion

    #region variables

    [Header("\nDriving Simulation Variables")]
    public float torque;
    public float BreakForce;
    public float turbo;

    private bool isBraking;
    private float breakForce;

    [Header("\nComponents")]
    public Rigidbody rb;

    #endregion

    #region Unity Methods

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
        ApplyBrake();
        UpdateWheelGraphics();
    }

    #endregion

    #region simulation


    void ApplyBrake() {
        if (isBraking){
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
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            isBraking = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            torque += turbo;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            torque -= turbo;
        }
    }

    #endregion

    #region graphic


    void UpdateWheelGraphics() {
        for (int i = 0; i < wheelGraphics.Length; i++) {
            allWheels[i].GetWorldPose(out Vector3 pos, out Quaternion rotation);

            wheelGraphics[i].transform.position = pos;
            wheelGraphics[i].transform.rotation = rotation;
        }
    }

    #endregion

    #region debugging

    [Header("\nDebugging")]
    public bool EnablePhysicalDebugging;
    public bool DrawCarForwardDirection;
    public bool DrawOnWheelPosition;
    public bool DrawCarUppwardsDirection;

    [Header("")]
    public bool EnableGUIDebugging;
    public bool EnableVectorDebugging;

    private void OnDrawGizmos() 
    {
        if (EnablePhysicalDebugging) 
        {
            if (DrawCarForwardDirection) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.Find("front part").position, transform.position + transform.forward * 10);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(transform.position + transform.forward * 10, Vector3.one / 5);
            }

            if (DrawOnWheelPosition) {
                foreach (WheelCollider wheel in allWheels) {
                    if (wheel != null) {
                        Gizmos.DrawCube(wheel.gameObject.transform.position, Vector3.one / 3);
                    }
                }
            }

            if (DrawCarUppwardsDirection) {
                Gizmos.color = Color.red;
                foreach(WheelCollider wheel in allWheels) {
                    Gizmos.DrawLine(wheel.gameObject.transform.position, wheel.gameObject.transform.position + wheel.gameObject.transform.up);
                }

                Gizmos.DrawLine(transform.position, transform.position + transform.up);
            }
        }

        if(EnableGUIDebugging) {
            if (EnableVectorDebugging) {
                Canvas GuiCanvas = GameObject.Find("Debug Canvas").GetComponent<Canvas>();
                TextMeshProUGUI text = GuiCanvas.gameObject.transform.Find("Vector Debugging Text").GetComponent<TextMeshProUGUI>();

                text.fontSize = 15;
                text.color = Color.red;
                text.text = "Velocity: " + rb.velocity + "\n" + 
                    "Speed: " + rb.velocity.magnitude + "\n" +
                    "World Pos: " + transform.position + "\n" +
                    "Forward Vector: " + transform.forward + "\n" + 
                    "Right Vector: " + transform.right;
            }
        }
    }

    #endregion
}
