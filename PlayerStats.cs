using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public static PlayerStats instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        { 
            Debug.Log("Deleted excess player stats from scene.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [Header("Player Stats")]
    private int currentLevel;
    public int maxHP = 100;
    public int currentHP = 100;
    public int maxMP = 100; 
    public int currentMP = 100;
    public int STR, INT, DEF, LCK, EXP, GOLD; 
    //
    [Header("Collected & Unlocked")]
    public SerializableDictionary<string, bool> abilitesUnlocked;
    public List<string> itemsCollected;
    //
    [SerializeField] private GameObject levelUpText;
    //
    public int[] levelReq = new int[] 
    { 0, 100, 250, 450, 700, 1000, 1350, 1750, 2200, 2700, // 1-10
    3250, 3850, 4500, 5200, 5950, 6750, 7600, 8500, 9450, 10450, // 11-20
    11700, 13200, 15100, 17500, 20400, 23700, 27200, 30900, 35000, 39500, // 21-30
    44500, 50000, 56000, 61500, 68500, 76000, 84000, 92500, 101500, 110000 }; // 31-40
    
    public int GetExpForNextLevel(int EXP, int[] levelReq)
    {
        for (int i = 0; i < levelReq.Length; i++)
        {
            if (levelReq[i] > EXP)
            {
                return (levelReq[i] - EXP);
            }
        }
        return 0; // max level
    }
    public int GetCurrentLevel(int EXP, int[] levelReq)
    {
        for (int i = 0; i < levelReq.Length; i++)
        {
            if (levelReq[i] > EXP)
            {
                return (i);
            }
        }
        return 40; // max level
    }
    public void LoadData(GameData data) // called on new game and load game
    {
        this.EXP = data.EXP;
        this.GOLD = data.GOLD;      
        this.abilitesUnlocked = data.abilitesUnlocked;
        this.itemsCollected = data.itemsCollected;
        ScaleStatsToLevel();
    }
    public void SaveData(ref GameData data) 
    { 
        data.EXP = this.EXP;
        data.GOLD = this.GOLD;
        data.abilitesUnlocked = this.abilitesUnlocked;
        data.itemsCollected = this.itemsCollected;
    }
    private void LevelUp() 
    {
    
        PauseGame();
        levelUpText.SetActive(true);
        StartCoroutine(RealTimeInvoke(1f));
        
    }
    public void GainExperience(int experience)
    {
        currentLevel = GetCurrentLevel(EXP, levelReq); // check current level
        EXP += experience; // add experience
        if (currentLevel < GetCurrentLevel(EXP, levelReq)) // check if player reached next level
        {
            LevelUp();
        }
        ScaleStatsToLevel();
    }
    public void ScaleStatsToLevel()
    {
        int level = GetCurrentLevel(EXP, levelReq);
        // set stats according to level scaling
        maxHP = (90 + (level * 10) + (GetCollectedHealthUp() * 10));
        maxMP = (90 + (level * 10) + (GetCollectedManaUp() * 10));
        currentHP = maxHP; // current HP/MP was restored on save
        currentMP = maxMP;
        STR = (8 + (level * 2));
        INT = (9 + level);
        DEF = (9 + level);
        LCK = (9 + level);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { // decreasing current HP for testing purposes
            currentHP -= 10;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(GetCollectedHealthUp());
        }
    }
    private int GetCollectedHealthUp()
    {   
        int result = 0;
        if (itemsCollected.Count > 0)
        {
            foreach (string item in itemsCollected)
            {
                if (!string.IsNullOrEmpty(item) && item.Substring(0, 1) == "h")
                {
                    result++;
                }
            }
        }   
        return result;
    }
    private int GetCollectedManaUp()
    {   
        int result = 0;
        if (itemsCollected.Count > 0)
        {
            foreach (string item in itemsCollected)
            {
                if (!string.IsNullOrEmpty(item) && item.Substring(0, 1) == "m")
                {
                    result++;
                }
            }
        }   
        return result;
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void UnPauseGame()
    {
        Time.timeScale = 1;
        Debug.Log("Unpause");
    }
    private IEnumerator<WaitForSecondsRealtime> RealTimeInvoke(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        levelUpText.SetActive(false);
        UnPauseGame();
    }    
}