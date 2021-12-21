using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path : MonoBehaviour
{
    [SerializeField, HideInInspector]
    List<Vector2> Points;
    [SerializeField, HideInInspector]
    bool IsClosed;
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

    public Vector2 this[int i] {
        get {
            return Points[i];
        }
    }

    public int NumSegments {
        get {
            return Points.Count / 3;
        }
    }

    public int NumOfPoits {
        get {
            return Points.Count;
        }
    }

    public void AddSegment(Vector2 anchorPos) {
        Points.Add(Points[Points.Count - 1] * 2 - Points[Points.Count - 2]);
        Points.Add((Points[Points.Count - 1] + anchorPos) * .5f);
        Points.Add(anchorPos);
    }

    public Vector2[] GetPointsInSegment(int i) {
        return new Vector2[] {
            Points[i*3],
            Points[i*3 + 1],
            Points[i*3 + 2],
            Points[LoopIndex(i*3 + 3)]
        };
    }


    public void MovePoint(int i, Vector2 pos) {
        Vector2 deltaMove = pos - Points[i];
        Points[i] = pos;

        if (i % 3 == 0) {
            if (i + 1 < Points.Count || IsClosed) {
                Points[LoopIndex(i + 1)] += deltaMove;
            }
            if (i - 1 <= 0 || IsClosed) {
                Points[LoopIndex(i - 1)] += deltaMove;
            }
        } else {
            bool NextIsAnchor = (i + 1) % 3 == 0;
            int CorrespondingControlIndex = NextIsAnchor ? i + 2 : i - 2;
            int anchorIndex = NextIsAnchor ? i + 1 : i - 1;

            if (CorrespondingControlIndex >= 0 && CorrespondingControlIndex < Points.Count) {
                float distance = (Points[LoopIndex(anchorIndex)] - Points[CorrespondingControlIndex]).magnitude;
                Vector2 dir = (Points[LoopIndex(anchorIndex)] - pos).normalized;
                Points[LoopIndex(CorrespondingControlIndex)] = Points[LoopIndex(anchorIndex)] + dir * distance;
            }
        }
     }

    public void ToggleClosed() {
        IsClosed = !IsClosed;

        if(IsClosed) {
            Points.Add(Points[Points.Count - 1] * 2 - Points[Points.Count - 2]);
            Points.Add(Points[0] * 2 - Points[1]);
        } else {
            Points.RemoveRange(Points.Count - 2, 2);
        }
    }

    private int LoopIndex(int i) {
        return (i + Points.Count) % Points.Count;
    }
}
