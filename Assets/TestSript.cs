using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class TestSript : MonoBehaviour
{
    public Vector3[] path;
    private void OnDrawGizmos() {
        foreach(Vector3 v3 in path) {
            Gizmos.DrawCube(v3, Vector3.one);
        }
    }
}
