using UnityEngine;

public class FollowObject : MonoBehaviour
{
	public Transform Target;
	public float DampTime = 0.2f;
	private Camera _camera;
	private Vector3 _moveVelocity;


	void Awake ()
	{
		transform.position = GetTargetPosition();
		_camera = GetComponentInChildren<Camera>();
	}
	
	void Update () {
		Move();
	}

	void Move()
	{
		var targetPosition = GetTargetPosition();
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _moveVelocity, DampTime);
	}

	private Vector3 GetTargetPosition()
	{
		var targetPosition = Target.position;
		return new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
	}
}
