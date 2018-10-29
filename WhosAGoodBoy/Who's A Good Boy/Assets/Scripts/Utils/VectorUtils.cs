using System;
using UnityEngine;

public static class VectorExtensions {

    public static Vector2 Sign(this Vector2 vec)
    {
        return new Vector2(Math.Sign(vec.x), Math.Sign(vec.y));
    }

    public static Vector3 Sign(this Vector3 vec)
    {
        return new Vector3(Math.Sign(vec.x), Math.Sign(vec.y), Math.Sign(vec.z));
    }

    public static Vector3 ApplyDirectionMask(this Vector3 toMask, Vector3 mask)
    {
        return new Vector3(sameSign(toMask.x, mask.x) ? 0 : toMask.x,
            sameSign(toMask.y, mask.y) ? 0 : toMask.y,
            sameSign(toMask.z, mask.z) ? 0 : toMask.z);

    }

    private static bool sameSign(float num1, float num2)
    {
        return num1 > 0 && num2 > 0 || num1 < 0 && num2 < 0;
    }
}