using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Interactable))]
public class MinigameLoader : MonoBehaviour {

    private Interactable _interactable;
    [SerializeField]
    private string _sceneNameToLoad;
    [SerializeField]
    [Range(1f, 10f)]
    private int _minigameDifficulty = 5;

    [SerializeField]
    private MinigameRewards _rewards;
    [SerializeField]
    private MinigamePenalties _penalities;

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
        LevelManager.Instance.MinigameDifficulty = _minigameDifficulty;
        LevelManager.Instance.MinigameRewards = _rewards;
        LevelManager.Instance.MinigamePenalties = _penalities;
        LevelManager.Instance.LoadMinigame(_sceneNameToLoad);
    }
}
