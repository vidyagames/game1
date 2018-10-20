using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Interactable
{
    private SpriteRenderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.color = _defaultColor;
    }

    public override void StartInteraction()
    {
        Debug.Log("Started interaction with house " + gameObject.name);
        myRenderer.color = _interactionColor;
    }

    public override void ShowAsInteractable()
    {
        Debug.Log(gameObject.name + " is house interactable");
        myRenderer.color = _interactableColor;
    }

    public override void UnshowAsInteractable()
    {
        Debug.Log(gameObject.name + " is house uninteractable");
        myRenderer.color = _defaultColor;
    }
}
