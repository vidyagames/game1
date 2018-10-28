using System.Collections.Generic;
using UnityEngine;

public static class CardinalUtils {
    private static readonly Dictionary<Vector2, Cardinal> VecToCardinal = new Dictionary<Vector2, Cardinal> {
        {Vector2.up, Cardinal.N},
        {Vector2.right, Cardinal.E},
        {Vector2.down, Cardinal.S},
        {Vector2.left, Cardinal.W}
    };

    private static readonly Dictionary<Cardinal, Vector2> CardinalToVec = new Dictionary<Cardinal, Vector2>
    {
        {Cardinal.N, Vector2.up},
        {Cardinal.E, Vector2.right},
        {Cardinal.S, Vector2.down},
        {Cardinal.W, Vector2.left}
    };

    public static Vector2 ToVector2(this Cardinal cardinal)
    {
        return CardinalToVec[cardinal];
    }

    public static Cardinal ToCardinal(this Vector2 vec)
    {
        return VecToCardinal[vec];
    }

    public static bool IsCardinal(this Vector2 vec)
    {
        return VecToCardinal.ContainsKey(vec);
    }
}