using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsManager : MonoBehaviour {

    [SerializeField]
    private Sprite _defaultIcon_interactable;
    [SerializeField]
    private Sprite _defaultIcon_interacting;
    [SerializeField]
    private GameObject _defaultParticleSystem_focus;

    public static InteractionsManager Instance;

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public Sprite GetDefaultInteractableSprite()
    {
        return Instance._defaultIcon_interactable;
    }
    public Sprite GetDefaultInteractingSprite()
    {
        return Instance._defaultIcon_interacting;
    }
    public GameObject GetDefaultFocusPS()
    {
        return Instance._defaultParticleSystem_focus;
    }
}
