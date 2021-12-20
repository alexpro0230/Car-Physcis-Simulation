using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path : MonoBehaviour
{
    [SerializeField, HideInInspector]
    List<Vector2> Points;

    public Path(Vector2 Centre)
    {
        Points = new List<Vector2>
        {
            Centre + Vector2.left,
            Centre + (Vector2.left + Vector2.up) * .5f,
            Centre + (Vector2.right + Vector2.down) * .5f,
            Centre + Vector2.right
        };
    }

    public void AddSegment(Vector2 anchorPos) {
        Points.Add(Points[Points.Count - 1] * 2 - Points[Points.Count - 2]);
        Points.Add((Points[Points.Count - 1] + anchorPos) * .5f);
        Points.Add(anchorPos);
    }
}
