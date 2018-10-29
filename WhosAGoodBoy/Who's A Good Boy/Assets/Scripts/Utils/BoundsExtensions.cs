using UnityEngine;

public static class BoundsExtensions {
    public static bool Contains(this Bounds bound, Bounds other)
    {
        return bound.Contains(other.max) && bound.Contains(other.min);
    }

    // Return the vector that describes how far outside the bound is
    // such that if the vector was applied to the second bound's center as a transformation,
    // it would be lined up with this bound on its furthest side.
    public static Vector3 Delta(this Bounds inner, Bounds outer)
    {
        var maxDifference = Vector3.Min(outer.max - inner.max, Vector3.zero);
        var minDifference = Vector3.Max(outer.min - inner.min, Vector3.zero);
        return minDifference + maxDifference ;
    }
}
