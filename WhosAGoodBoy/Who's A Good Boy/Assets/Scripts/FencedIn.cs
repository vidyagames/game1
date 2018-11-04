using UnityEngine;

public class FencedIn : MonoBehaviour {
    private bool _previouslyOutOfBounds;

    public Bounds Fence;
    public Bounds FencedBound;
    public bool ShowBounds;

    public delegate void OutOfBoundCallback(Vector3 delta);
    public event OutOfBoundCallback OnOutOfBounds;

    public delegate void BackInBoundsCallback();
    public event BackInBoundsCallback OnBackInBounds;


    private void Awake()
    {
        _previouslyOutOfBounds = false;
    }

    private void Update()
    {
        CheckOutOfBoundsState();
    }

    private void OnDrawGizmos()
    {
        if (!ShowBounds) {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Fence.center, Fence.size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(FencedBound.center, FencedBound.size);
    }

    private void CheckOutOfBoundsState()
    {
        bool outOfBounds = !IsInBounds();

        if (outOfBounds) {
            HandleOutOfBounds();
            return;
        }

        if (_previouslyOutOfBounds) {
            HandleBackInBounds();
        }
    }

    private void HandleBackInBounds()
    {
        FireBackInBoundsEvent();
        _previouslyOutOfBounds = false;
    }

    private void HandleOutOfBounds()
    {
        FireOutOfBoundsEvent();
        _previouslyOutOfBounds = true;
    }

    private bool IsInBounds()
    {
        return Fence.Contains(FencedBound);
    }

    private void FireOutOfBoundsEvent()
    {
        if (OnOutOfBounds != null) {
            OnOutOfBounds(FencedBound.Delta(Fence));
        }
    }

    private void FireBackInBoundsEvent()
    {
        if (OnBackInBounds != null) {
            OnBackInBounds();
        }
    }
}
