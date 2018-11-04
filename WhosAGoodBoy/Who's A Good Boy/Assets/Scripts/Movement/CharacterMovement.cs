using UnityEngine;

public class CharacterMovement : MonoBehaviour, CardinalMovement {
    private static readonly float DEFAULT_SPEED = 3;

    [SerializeField]
    public float Speed { get; private set; }

    public bool IsMoving { get; private set; }

    public Cardinal Facing { get; set; }

    void Start()
    {
        IsMoving = false;
        Speed = DEFAULT_SPEED;
    }

    void Update()
    {
        DoMove();
    }

    public void Move(Cardinal cardinal)
    {
        IsMoving = true;
        Facing = cardinal;
    }

    public void Stop()
    {
        IsMoving = false;
    }

    private void DoMove()
    {
        transform.Translate(Facing.ToVector2() * Time.deltaTime * (IsMoving ? Speed : 0));
    }

}

