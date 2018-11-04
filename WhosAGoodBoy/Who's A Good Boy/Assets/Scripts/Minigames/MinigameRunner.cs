using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameRunner : MonoBehaviour {
    public string GameName;
    public virtual void Lost()
    {
        LevelManager.Instance.LostMinigame();
        Debug.Log("Lost Minigame: " + GameName + ". Difficulty: " + LevelManager.Instance.MinigameDifficulty);
    }

    public virtual void Won()
    {
        LevelManager.Instance.WonMinigame();
        Debug.Log("Won Minigame: " + GameName + ". Difficulty: " + LevelManager.Instance.MinigameDifficulty);
    }
}
