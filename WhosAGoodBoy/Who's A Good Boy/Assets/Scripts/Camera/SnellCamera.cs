using UnityEngine;

[RequireComponent(typeof(FencedIn))]
public class SnellCamera : MonoBehaviour
{
	public FencedIn Fence;
	public TransformFollower Follower;
	public Camera BoundCamera;

	private void Awake()
	{
		BoundCamera = GetComponentInChildren<Camera>();
		UpdateFenceBounds();
	}

	private void OnEnable()
	{
		Fence.OnOutOfBounds += OnOutOfBounds;
		Fence.OnBackInBounds += OnBackInBounds;
	}

	private void OnDisable()
	{
		Fence.OnOutOfBounds -= OnOutOfBounds;
	}

	private void Update()
	{
		UpdateFenceBounds();
	}

	private void UpdateFenceBounds()
	{
		Fence.FencedBound = BoundCamera.OrthographicBounds();
	}

	private void OnOutOfBounds(Vector3 delta)
	{
		var movementMask = -delta.Sign();
		Follower.MovementMask = movementMask;
	}

	private void OnBackInBounds()
	{
		Follower.MovementMask = Vector3.zero;
	}
}
