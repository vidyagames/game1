using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Interactable))]
public class SceneLoader : MonoBehaviour {

    private Interactable _interactable;
    [SerializeField]
    private string _sceneNameToLoad;

    void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.Interacted += OnInteractedWith;
    }

    void OnDestroy()
    {
        _interactable.Interacted -= OnInteractedWith;
    }


    private void OnInteractedWith(object sender, InteractedWithEventArgs args)
    {
        SavedGameManager.Instance.Save();
        SceneManager.LoadScene(_sceneNameToLoad);
    }
}
