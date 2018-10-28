using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunctions {

    public static string OverWorldSceneName = "SampleScene";

    public static void LoadSceneThenLoadSaveState(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SavedGameManager.Instance.Load();
    }
}
