using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour {
    #region forces

    public Vector3 Ftraction;
    public Vector3 Fdrag;
    public Vector3 Frr;
    public Vector3 Flong;
    #endregion

    [Header("\n\n")]

    #region vectors

    public Vector3 u;
    public Vector3 velocity;
    public Vector3 acceleration;
    #endregion

    [Header("\n\n")]


    #region Constants

    public float EngineForce;
    public float CarMass;


    public float Cdrag;
    public float Crr;

    public bool SetDefaultConstants;
    #endregion

    private void Start() {
        
        //foreach (Transform child in transform) {
        //    child.gameObject.GetComponent<WheelCollider>().motorTorque = 500;
        //}
        
        velocity = 
            u = 
            acceleration =
            Fdrag =
            Frr =
            Flong =
            new Vector3();

        if(SetDefaultConstants) {
            Cdrag = 0.5f * 0.3f * 2.2f * 1.29f; // 0.4257
            Crr = 30 * Cdrag;
        }

        rb = GetComponent<Rigidbody>();
    }

    Rigidbody rb;
    private void Update() {
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        u = transform.forward * inputDirection.y + transform.right * inputDirection.x;

        Ftraction = u * EngineForce;
        Fdrag = -Cdrag * rb.velocity * rb.velocity.magnitude;
        Frr = -Crr * rb.velocity;
        Flong = Ftraction + Fdrag + Frr;
        acceleration = Flong;
        velocity += acceleration * Time.deltaTime;

        rb.velocity = velocity;
    }
}
