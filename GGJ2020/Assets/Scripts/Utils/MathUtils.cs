using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{
    public static Vector3 VectorLerp(Vector3 a, Vector3 b, float t)
    {
        return Vector3.forward * Mathf.Lerp(a.z, b.z, t) + Vector3.right * Mathf.Lerp(a.x, b.x, t) + Vector3.up * Mathf.Lerp(a.y, b.y, t);
    }

    public static Vector2 VectorLerp(Vector2 a, Vector2 b, float t)
    {
        return Vector2.right * Mathf.Lerp(a.x, b.x, t) + Vector2.up * Mathf.Lerp(a.y, b.y, t);
    }
}
