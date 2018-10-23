using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public CharacterMovement Movement;

    // We have to keep track of previous input and direction to handle edge cases with input and cardinal movement
    // (See CardinalUtils)
    private Vector2 _prevAxes;
    private Cardinal _prevCardinal;

    void Update()
    {
        var axes = GetAxes();
        var moveCardinal = GetNextCardinal();

        if (ShouldStopMoving(axes)) {
            Movement.Stop();
        }
        else {
            Movement.Move(moveCardinal);
        }

        _prevAxes = axes;
        _prevCardinal = moveCardinal;
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
        Cardinal? cardinalChange = CardinalUtils.GetChange(axes, _prevAxes);
        return cardinalChange ?? _prevCardinal;
    }
}
