using System.IO;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData {
    public string currentLevel;
    public Vector3 lastOverworldPosition;
    public int lastOverworldFacing;
}

public class SavedGameManager : MonoBehaviour {
    private SaveData saveData;
    private PlayerController playerController;
    string dataPath;

    public static SavedGameManager Instance;

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        saveData = new SaveData();

        //Get Player Controller in scene
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "SavedGame.txt");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Save();

        if (Input.GetKeyDown(KeyCode.P)) {
            Load();
        }
    }

    void SetData()
    {
        if (LevelManager.Instance.overworldSceneName == SceneManager.GetActiveScene().name)
        {
            saveData.lastOverworldPosition = playerController.transform.position;
            saveData.lastOverworldFacing = playerController.GetFacing().ToInt();
        }
    }

    void SetStates()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (LevelManager.Instance.overworldSceneName == currentScene.name) {
            playerController.transform.position = saveData.lastOverworldPosition;
            playerController.SetFacing(saveData.lastOverworldFacing.ToCardinal());
        }
    }

    public void Save()
    {
        Debug.Log("Saving to File");
        SetData();
        string jsonString = JsonUtility.ToJson(saveData);

        using (StreamWriter streamWriter = File.CreateText(dataPath)) {
            streamWriter.Write(jsonString);
        }
    }

    public void LoadFromFile()
    {
        Debug.Log("Loading from File");
        using (StreamReader streamReader = File.OpenText(dataPath)) {
            string jsonString = streamReader.ReadToEnd();
            saveData = JsonUtility.FromJson<SaveData>(jsonString);
        }
        SetStates();
    }

    public void Load()
    {
        SetStates();
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Load();
    }
}