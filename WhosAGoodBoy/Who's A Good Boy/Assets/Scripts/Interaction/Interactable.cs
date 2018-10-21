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

    public event EventHandler BecameCurrentInteractable;
    public event EventHandler LostCurrentInteractable;
    public event EventHandler<InteractedWithEventArgs> InteractedWith;

    public void SetAsCurrentInteractable()
    {
        if (currentInteractable != this)
        {
            if (currentInteractable != null)
                currentInteractable.UnshowAsInteractable();
            currentInteractable = this;
            ShowAsInteractable();
        }
    }
    public static void ClearCurrentInteractable()
    {
        if (currentInteractable != null)
            currentInteractable.UnshowAsInteractable();
        currentInteractable = null;
    }

    public static void InteractWithCurrentObject(InteractionType interactionType)
    {
        if (currentInteractable != null)
            currentInteractable.StartInteraction(interactionType);
    }

    public virtual void StartInteraction(InteractionType interactionType)
    {
        InteractedWith.Raise(this, new InteractedWithEventArgs() { InteractionType = interactionType });
    }

    public virtual void ShowAsInteractable()
    {
        BecameCurrentInteractable.Raise(this, EventArgs.Empty);
    }

    public virtual void UnshowAsInteractable()
    {
        LostCurrentInteractable.Raise(this, EventArgs.Empty);
    }
}
