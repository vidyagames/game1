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
    private InteractionType _defaultInteractionType = InteractionType.Use;

    public InteractionType GetDefaultInteractionType()
    {
        return _defaultInteractionType;
    }

    [SerializeField]
    private bool _useGenericInteractionIndicators;
    [SerializeField]
    private Sprite _focusSprite;
    [SerializeField]
    private Sprite _interactingSprite;
    private GameObject _interactionIndicator;
    private SpriteRenderer _interactionIndicatorRend;
    private ParticleSystem _interactionIndicatorPS;

    public void Start()
    {
        if (_focusSprite == null)
            _focusSprite = InteractionsManager.Instance.GetDefaultInteractableSprite();
        if (_interactionIndicator == null)
            _interactingSprite = InteractionsManager.Instance.GetDefaultInteractingSprite();
    }

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
            _interactionIndicatorPS.Stop();
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
                _interactionIndicatorPS = Instantiate(InteractionsManager.Instance.GetDefaultFocusPS()).GetComponent<ParticleSystem>();
                _interactionIndicatorPS.transform.localPosition = Vector3.zero;
                _interactionIndicatorPS.transform.SetParent(_interactionIndicator.transform);
                _interactionIndicatorRend = _interactionIndicator.AddComponent<SpriteRenderer>();
                _interactionIndicator.transform.SetParent(gameObject.transform);
                _interactionIndicator.transform.localPosition = Vector3.zero;
                _interactionIndicatorRend.sortingLayerID = GetComponent<SpriteRenderer>().sortingLayerID;
                _interactionIndicatorRend.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            _interactionIndicatorRend.sprite = _focusSprite;
            _interactionIndicatorPS.Play();
        }
    }

    public void LoseFocus()
    {
        LostFocus.Raise(this, EventArgs.Empty);
        if (_useGenericInteractionIndicators)
        {
            _interactionIndicatorRend.sprite = null;
            _interactionIndicatorPS.Stop();
        }
    }
}
