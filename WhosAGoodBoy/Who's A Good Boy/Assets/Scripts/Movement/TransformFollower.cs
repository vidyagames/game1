using UnityEngine;

public class TransformFollower : MonoBehaviour
{
    public Transform TransformToFollow;
    public float DampTime = 0.2f;
    public Vector3 MovementMask { get; set; }
    private Vector3 _moveVelocity;

    private void Awake ()
	{
        transform.position = GetTargetPosition();
        MovementMask = Vector3.zero;
	}

    private void Update () {
        Move();
	}

    private void Move()
	{
        var targetPosition = GetTargetPosition();
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _moveVelocity, DampTime);
	}

    private Vector3 GetTargetPosition()
    {
        var delta = ToTarget();
        var maskedDelta = delta.ApplyDirectionMask(MovementMask);
        return transform.position + maskedDelta;
    }

    private Vector3 ToTarget()
    {
        return TransformToFollow.position - transform.position;
    }
}
