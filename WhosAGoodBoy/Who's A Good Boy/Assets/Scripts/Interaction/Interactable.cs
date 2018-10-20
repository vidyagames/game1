using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public static Interactable currentInteractable;

    protected Color _interactableColor = new Color(1f, 1f, 1f, .25f);
    protected Color _defaultColor = new Color(1f, 1f, 1f, 0f);
    protected Color _interactionColor = new Color(.5f, .8f, .2f, .25f);

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

    public static void InteractWithCurrentObject()
    {
        if (currentInteractable != null)
            currentInteractable.StartInteraction();
    }

    public virtual void StartInteraction()
    {
        Debug.Log("Started interaction with " + gameObject.name);
    }

    public virtual void ShowAsInteractable()
    {
        Debug.Log(gameObject.name + " is interactable");
    }

    public virtual void UnshowAsInteractable()
    {
        Debug.Log(gameObject.name + " is uninteractable");
    }
}
