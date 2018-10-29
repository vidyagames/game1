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
        NotifySubscribers();
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

    private void NotifySubscribers()
    {
        bool outOfBounds = !IsInBounds();

        if (outOfBounds) {
            FireOutOfBoundsEvent();
            if (!_previouslyOutOfBounds) {
                _previouslyOutOfBounds = true;
            }
        }

        if (_previouslyOutOfBounds && !outOfBounds) {
            FireBackInBoundsEvent();
            _previouslyOutOfBounds = false;
        }

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
