using System;
using UnityEngine;

public static class VectorExtensions {

    public static Vector2 Sign(this Vector2 vec)
    {
        return new Vector2(Math.Sign(vec.x), Math.Sign(vec.y));
    }
}