using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCarFollow : MonoBehaviour {
    public Vector3 DistanceFromCar;
    private GameObject car;
    public float XAngle;
    public bool RotateCamera;

    private void Start() {
        car = GameObject.Find("car 1203 black");
    }

    private void Update() {
        CinemachineVirtualCamera VC = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;

        if (DistanceFromCar.z > 15 || DistanceFromCar.z < -15) {
            XAngle -= Time.deltaTime * 30;
        } else if(DistanceFromCar.z > 2 || DistanceFromCar.z < -2) {
            XAngle += Time.deltaTime * 30;
        }

        XAngle = Mathf.Clamp(XAngle, 15, 75);

        DistanceFromCar.z = -car.GetComponent<Rigidbody>().velocity.z;

        DistanceFromCar.z = Mathf.Clamp(DistanceFromCar.z, -25, -5);
        VC.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = -Mathf.Abs(DistanceFromCar.z);
        VC.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = Mathf.Abs(DistanceFromCar.z);
    }
}
