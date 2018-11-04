using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public string overworldSceneName;
    public string swimmingGameSceneName;

    public static LevelManager Instance;

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }
}
