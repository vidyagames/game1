using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public CharacterMovement Movement;

    private Vector2 _prevAxes;
    private Cardinal _prevCardinal;
    private Cardinal _curCardinal;

    void Update()
    {
        var axes = GetAxes();
        _curCardinal = GetNextCardinal();

        if (ShouldStopMoving(axes)) {
            Movement.Stop();
        }
        else {
            Movement.Move(_curCardinal);
        }

        _prevAxes = axes;
        _prevCardinal = _curCardinal;
    }

    private Vector2 GetAxes()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    bool ShouldStopMoving(Vector2 Axes)
    {
        float sqrMagnitude = Axes.SqrMagnitude();
        return sqrMagnitude < Single.Epsilon;
    }

    private Cardinal GetNextCardinal()
    {
        var axes = GetAxes();
        Cardinal? cardinalChange = GetDirectionChange(axes, _prevAxes);
        return cardinalChange ?? _prevCardinal;
    }

    public Cardinal GetFacing()
    {
        return Movement.Facing;
    }

    public void SetFacing(Cardinal direction)
    {
        Movement.Facing = direction;
    }

    /*
        Given a previous input and a current input, determine if there should be a change in
        direction. When a user is pressing more than one directional input, we have to resolve to a single direction.
        This isn't as trivial as it seems, and really comes down to preference, but this method weights
        the recent direction more (e.g. if already heading right and up is introduced, go up)
     */
    public static Cardinal? GetDirectionChange(Vector2 input, Vector2 prevInput)
    {
        // Get the sign vectors, which just tell us if *any* amount of x or y are present and in what direction
        var inputSignVec = input.Sign();
        var prevInputSignVec = prevInput.Sign();

        // If the user has stopped pressing inputs or is pressing contradictory inputs
        if (inputSignVec == Vector2.zero) {
            return null;
        }

        if (inputSignVec == prevInputSignVec) {
            return null;
        }

        // If the new input only has one dimension, there's no conflict to resolve
        if (inputSignVec.IsCardinal()) {
            return inputSignVec.ToCardinal();
        }

        /*
         Otherwise the input has two dimensions. Go in the newly introduced direction*/
        return (inputSignVec - prevInputSignVec).ToCardinal();
    }
}
