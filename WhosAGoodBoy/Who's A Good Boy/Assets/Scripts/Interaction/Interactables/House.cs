using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public class House : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    private Interactable interactable;

    protected Color _interactableColor = new Color(1f, 1f, 1f, .25f);
    protected Color _defaultColor = new Color(1f, 1f, 1f, 0f);
    protected Color _interactionColor = new Color(.5f, .8f, .2f, .25f);


    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.color = _defaultColor;
        interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        interactable.GainedFocus += OnGainedFocus;
        interactable.LostFocus += OnLostFocus;
        interactable.Interacted += OnInteractedWith;
    }

    private void OnDisable()
    {
        interactable.GainedFocus -= OnGainedFocus;
        interactable.LostFocus -= OnLostFocus;
        interactable.Interacted -= OnInteractedWith;
    }

    public void OnInteractedWith(object sender, InteractedWithEventArgs args)
    {
        Debug.Log("Started " + args.InteractionType + " with house " + gameObject.name);
        myRenderer.color = _interactionColor;
    }

    public void OnGainedFocus(object sender, EventArgs args)
    {
        Debug.Log(gameObject.name + " is house interactable");
        myRenderer.color = _interactableColor;
    }

    public void OnLostFocus(object sender, EventArgs args)
    {
        Debug.Log(gameObject.name + " is house uninteractable");
        myRenderer.color = _defaultColor;
    }
}
