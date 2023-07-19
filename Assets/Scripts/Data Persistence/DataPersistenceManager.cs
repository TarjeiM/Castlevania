using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement; 

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName = "savegame.json";
    private FileDataHandler dataHandler;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        { 
            Debug.Log("Deleted excess data persistence manager from scene.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // find all data persistence objects on scene load
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private void OnSceneUnloaded(Scene scene) {
        // called on scene exit
    }

    private int loadCheck;
    private void Start() {
        loadCheck = 0;
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            NewGame();
            loadCheck = 1;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            LoadGame();
            loadCheck = 1;
        }

        if (Input.GetKeyUp(KeyCode.Alpha3)) 
        {
            if (loadCheck == 1) {
                SaveGame();
            }
            else {
                Debug.Log("You cannot save before loading or starting a new game");
            }
        }
    }

    public void NewGame() {
        this.gameData = new GameData(); // create new instance of GameData
        dataHandler.Save(gameData); // write data from the new instance to file
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData); // load the new data
        }
        Debug.Log("New Game");
    }

    public void LoadGame() {
        // load any saved data from file using data handler
        this.gameData = dataHandler.Load();
        // if there is no save data, print an error message to console
        if (this.gameData == null) {
            Debug.LogError("No save data found.");
            return;
        }
        // push the loaded data to scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded Game");
    }

    public void SaveGame() {
        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistanceObj in dataPersistenceObjects) {
            dataPersistanceObj.SaveData(ref gameData);
        }
        // save that data to a file using the data handler
        dataHandler.Save(gameData);
        Debug.Log("Saved Game");
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistenceObjects = 
        FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
