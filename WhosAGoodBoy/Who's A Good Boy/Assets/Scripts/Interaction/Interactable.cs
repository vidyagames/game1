using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractedWithEventArgs : EventArgs
{
    public InteractionType InteractionType;
}

public enum InteractionType { Sniff, Use, Attack}

public class Interactable : MonoBehaviour {

    private static Interactable currentInteractable_internal;
    public static Interactable CurrentInteractable
    {
        set
        {
            if (currentInteractable_internal != value)
            {
                if (currentInteractable_internal != null)
                    currentInteractable_internal.LoseFocus();
                currentInteractable_internal = value;
                if (currentInteractable_internal != null)
                    currentInteractable_internal.GainFocus();
            }
        }
        get { return currentInteractable_internal; }
    }

    public event EventHandler GainedFocus;
    public event EventHandler LostFocus;
    public event EventHandler<InteractedWithEventArgs> Interacted;

    [SerializeField]
    private bool _useGenericInteractionIndicators;
    [SerializeField]
    private Sprite _focusSprite;
    [SerializeField]
    private Sprite _interactingSprite;
    private GameObject _interactionIndicator;
    private SpriteRenderer _interactionIndicatorRend;

    public static void ClearCurrentInteractable()
    {
        if (CurrentInteractable != null)
            CurrentInteractable.LoseFocus();
        CurrentInteractable = null;
    }

    public static void InteractWithCurrentObject(InteractionType interactionType)
    {
        if (CurrentInteractable != null)
            CurrentInteractable.StartInteraction(interactionType);
    }

    public void StartInteraction(InteractionType interactionType)
    {
        Interacted.Raise(this, new InteractedWithEventArgs() { InteractionType = interactionType });
        if (_useGenericInteractionIndicators)
        {
            _interactionIndicatorRend.sprite = _interactingSprite;
        }
    }

    public void GainFocus()
    {
        GainedFocus.Raise(this, EventArgs.Empty);
        if (_useGenericInteractionIndicators)
        {
            if (_interactionIndicator == null)
            {
                _interactionIndicator = new GameObject("interaction indicator");
                _interactionIndicatorRend = _interactionIndicator.AddComponent<SpriteRenderer>();
                _interactionIndicator.transform.SetParent(gameObject.transform);
                _interactionIndicator.transform.localPosition = Vector3.zero;
                _interactionIndicatorRend.sortingLayerID = GetComponent<SpriteRenderer>().sortingLayerID;
                _interactionIndicatorRend.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            _interactionIndicatorRend.sprite = _focusSprite;
        }
    }

    public void LoseFocus()
    {
        LostFocus.Raise(this, EventArgs.Empty);
        if (_useGenericInteractionIndicators)
        {
            _interactionIndicatorRend.sprite = null;
        }
    }
}
