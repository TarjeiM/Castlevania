using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    [Header("Player Stats")]
    public int maxHP = 100;
    public int currentHP = 100;
    public int maxMP = 100; 
    public int currentMP = 100;
    public int STR, INT, DEF, LCK, EXP, GOLD; 
    //
    [Header("Collected & Unlocked")]
    public SerializableDictionary<string, bool> abilitesUnlocked;
    public SerializableDictionary<string, int> itemsCollected;
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
        // retrieve saved data
        this.EXP = data.EXP;
        this.GOLD = data.GOLD;
        ScaleStatsToLevel();
        
        this.abilitesUnlocked = data.abilitesUnlocked;
        this.itemsCollected = data.itemsCollected;
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
        // pause? wait for input?
        levelUpText.SetActive(true);
        ScaleStatsToLevel();
    }
    public void GainExperience(int experience)
    {
        EXP += experience;
        // check for level up?
        ScaleStatsToLevel();
    }
    private void ScaleStatsToLevel()
    {
        int level = GetCurrentLevel(EXP, levelReq);
        // set stats according to level scaling
        maxHP = (90 + (level * 10));
        maxMP = (90 + (level * 10));
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
    }
}
