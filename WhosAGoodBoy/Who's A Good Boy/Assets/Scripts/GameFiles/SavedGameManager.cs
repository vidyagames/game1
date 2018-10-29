using System.IO;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class CharacterData {
    public Vector3 lastOverworldPosition;
    public int facing;
}

public class SavedGameManager : MonoBehaviour {
    public CharacterData characterData;
    string dataPath;

    public static SavedGameManager Instance;

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "SavedGame.txt");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            SaveGameData(characterData, dataPath);

        if (Input.GetKeyDown(KeyCode.P)) {
            characterData = LoadGameData(dataPath);
            SetStates(characterData);
        }
    }

    static void SetData(ref CharacterData data)
    {
        if (SceneFunctions.OverWorldSceneName == SceneManager.GetActiveScene().name)
            data.lastOverworldPosition = PlayerController.Player.transform.position;
        data.facing = PlayerController.Player.GetFacing().ToInt();
    }

    static void SetStates(CharacterData data)
    {
        if (SceneFunctions.OverWorldSceneName == SceneManager.GetActiveScene().name)
            PlayerController.Player.transform.position = data.lastOverworldPosition;
        PlayerController.Player.SetFacing(data.facing.ToCardinal());
    }

    public void Save()
    {
        SaveGameData(characterData, dataPath);
    }

    public void LoadFromFile()
    {
        LoadGameData(dataPath);
        SetStates(characterData);
    }

    public void Load()
    {
        SetStates(characterData);
    }

    static void SaveGameData(CharacterData data, string path)
    {
        Debug.Log("Saving to File");
        SetData(ref data);
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path)) {
            streamWriter.Write(jsonString);
        }
    }

    static CharacterData LoadGameData(string path)
    {
        Debug.Log("Loading from File");
        using (StreamReader streamReader = File.OpenText(path)) {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<CharacterData>(jsonString);
        }
    }

    private void OnLevelWasLoaded()
    {
        if (Instance == this)
            Load();
    }
}