using System.Collections.Generic;

[System.Serializable]
public class GameData 
{
    // variable types we want to store in JSON
    public int EXP, GOLD;
    public SerializableDictionary<string, bool> abilitesUnlocked; // ability name, unlock status bool
    public List<string> itemsCollected; // item id

    // the values defined in this constructor will be the default starting values
    public GameData() {
        this.EXP = 0;
        this.GOLD = 0;
        this.abilitesUnlocked = new SerializableDictionary<string, bool>();
        this.itemsCollected = new List<string>();
    }
}

