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

    public static Interactable currentInteractable;

    public event EventHandler GainedFocus;
    public event EventHandler LostFocus;
    public event EventHandler<InteractedWithEventArgs> Interacted;

    public void SetAsCurrentInteractable()
    {
        if (currentInteractable != this)
        {
            if (currentInteractable != null)
                currentInteractable.LoseFocus();
            currentInteractable = this;
            GainFocus();
        }
    }
    public static void ClearCurrentInteractable()
    {
        if (currentInteractable != null)
            currentInteractable.LoseFocus();
        currentInteractable = null;
    }

    public static void InteractWithCurrentObject(InteractionType interactionType)
    {
        if (currentInteractable != null)
            currentInteractable.StartInteraction(interactionType);
    }

    public void StartInteraction(InteractionType interactionType)
    {
        Interacted.Raise(this, new InteractedWithEventArgs() { InteractionType = interactionType });
    }

    public void GainFocus()
    {
        GainedFocus.Raise(this, EventArgs.Empty);
    }

    public void LoseFocus()
    {
        LostFocus.Raise(this, EventArgs.Empty);
    }
}
