using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour {

    public string overworldSceneName;
    public string swimmingGameSceneName;

    public EventHandler MinigameWon;
    public EventHandler MinigameLost;

    [Range(1f, 10f)]
    public int MinigameDifficulty;

    public MinigameRewards MinigameRewards { get; set; }
    public MinigamePenalties MinigamePenalties { get; set; }

    public static LevelManager Instance;

    private PlayerController _playerController;

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        MinigameWon += OnMinigameWon;
        MinigameLost += OnMinigameLost;
    }

    private void OnDisable()
    {
        MinigameWon -= OnMinigameWon;
        MinigameLost -= OnMinigameLost;
    }

    private void OnMinigameWon(object sender, EventArgs args)
    {
        if (MinigameRewards != null) {
            if (MinigameRewards.Position != null)
                _playerController.transform.position = MinigameRewards.Position.position;
            MinigameRewards.OnWon.Invoke();
        }
    }

    private void OnMinigameLost(object sender, EventArgs args)
    {
        if (MinigamePenalties != null) {
            if (MinigamePenalties.Position != null)
                _playerController.transform.position = MinigamePenalties.Position.position;
            MinigamePenalties.OnLost.Invoke();
        }
    }

    public void WonMinigame()
    {
        StartCoroutine(UnloadCurrentMinigame(true));
    }

    public void LostMinigame()
    {
        StartCoroutine(UnloadCurrentMinigame(false));
    }

    private IEnumerator UnloadCurrentMinigame(bool minigameWon)
    {
        yield return StartCoroutine(UnloadCurrentScene());
        if (minigameWon) {
            MinigameWon.Raise(this, System.EventArgs.Empty);
        }
        else {
            MinigameLost.Raise(this, System.EventArgs.Empty);
        }
    }

    private IEnumerator UnloadCurrentScene()
    {
        if (SceneManager.GetSceneByName(overworldSceneName).isLoaded) {
            AsyncOperation op = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            yield return op;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(overworldSceneName));
            var rootGOs = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject GO in rootGOs)
                GO.SetActive(true);
        }
        else {
            Debug.LogWarning("Overworld scene not currently Loaded.");
            yield return null;
        }
    }

    public void LoadMinigame(string minigameName)
    {
        StartCoroutine(LoadSceneAdditively(minigameName));
    }

    private IEnumerator LoadSceneAdditively(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return op;
        var rootGOs = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject GO in rootGOs) {
            GO.SetActive(false);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
[Serializable]
public class MinigameRewards {
    public Transform Position;
    public NoParametersUnityEvent OnWon;
}
[Serializable]
public class MinigamePenalties {
    public Transform Position;
    public NoParametersUnityEvent OnLost;
}
[Serializable]
public class NoParametersUnityEvent : UnityEvent { }
