using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

    [SerializeField]
    private LayerMask _interactionLayers;
    private const float INTERACTION_FREQUENCY = .25f;
    [SerializeField]
    private float _interactionRadius = 1f;
    private float _timeSinceLastCheck;

    private Interactable _closestInteractableObject;

    public bool CheckForInteractions;

	// Use this for initialization
	void Start () {
        _timeSinceLastCheck = 0;
        CheckForInteractions = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (CheckForInteractions)
        {
            _timeSinceLastCheck += Time.deltaTime;
            if (_timeSinceLastCheck > INTERACTION_FREQUENCY)
            {
                _timeSinceLastCheck = 0f;
                CheckForInteractables();
            }
        }

        if (Input.GetButtonDown("Interact"))
        {
            Interactable.InteractWithCurrentObject(InteractionType.Use);
        }
	}

    private void CheckForInteractables()
    {
        Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(transform.position, _interactionRadius, _interactionLayers);
        if (interactableObjects.Length > 0)
        {
            Collider2D closestCollider = interactableObjects.GetClosest(transform.position);
            Interactable interactableObject = closestCollider.GetComponent<Interactable>();
            if (interactableObject != null)
            {
                _closestInteractableObject = interactableObject;
                Interactable.CurrentInteractable = _closestInteractableObject;
            }
        }
        else
        {
            Interactable.ClearCurrentInteractable();
        }
    }

    public void SetInteractionRadius(float newRadius)
    {
        _interactionRadius = newRadius;
    }
}
