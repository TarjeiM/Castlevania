using System.Collections.Generic;

[System.Serializable]
public class GameData 
{
    // variable types we want to store in JSON
    public int EXP, GOLD, maxPotion;
    public List<string> itemsCollected; // item id

    // the values defined in this constructor will be the default starting values
    public GameData() {
        this.maxPotion = 1;
        this.EXP = 0;
        this.GOLD = 0;
        this.itemsCollected = new List<string>();
    }
}

