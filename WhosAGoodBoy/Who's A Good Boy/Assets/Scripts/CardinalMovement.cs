public enum Cardinal
{
	N,
	E,
	S,
	W
}

public interface CardinalMovement {
    float Speed { get; }
    bool IsMoving { get; }
    Cardinal Facing { get; }
    void Move(Cardinal cardinal);
    void Stop();
}