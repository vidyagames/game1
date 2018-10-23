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

    /*
        Given a previous input and a current input, determine if there should be a change in
        direction. When a user is pressing more than one directional input, we have to resolve to a single direction.
        This isn't as trivial as it seems, and really comes down to preference, but this method weights
        the recent direction more (e.g. if already heading right and up is introduced, go up)
     */
    public static Cardinal? GetChange(Vector2 input, Vector2 prevInput)
    {
        var inputSignVec = input.Sign();
        var prevInputSign = prevInput.Sign();

        if (inputSignVec == Vector2.zero) {
            return null;
        }

        // There has been no change
        if (inputSignVec == prevInputSign) {
            return null;
        }

        // If the input only has one dimension, there's not conflict
        if (VecToCardinal.ContainsKey(inputSignVec)) {
            return VecToCardinal[inputSignVec];
        }

        /*
         Otherwise the input has two dimensions. Go in the newly introduced direction*/
        return VecToCardinal[inputSignVec - prevInputSign];
    }
}