[System.Serializable]
public class GameData 
{
    // variable types we want to store in JSON
    public int EXP, GOLD;
    public SerializableDictionary<string, bool> abilitesUnlocked; // ability name, unlock status bool
    public SerializableDictionary<string, int> itemsCollected; // item id, collected status bool

    // the values defined in this constructor will be the default starting values
    public GameData() {
        this.EXP = 0;
        this.GOLD = 0;
        this.abilitesUnlocked = new SerializableDictionary<string, bool>();
        this.itemsCollected = new SerializableDictionary<string, int>();
    }
}

