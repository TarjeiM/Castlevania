using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private bool isTesting = false;
    public static CanvasController instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        { 
            Debug.Log("Deleted excess canvas controller from scene.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // each section has a header followed by a reference to the parent object
    [Header("Test Display")]
    [SerializeField] private GameObject testDisplay;  
    [SerializeField] private TextMeshProUGUI textDirX, textDirY;
    private float dirX, dirY;
    // 
    [Header("Player HUD")]
    [SerializeField] private GameObject playerHUD; 
    [SerializeField] private TextMeshProUGUI textHealthHUD, textManaHUD;
    //
    [Header("Player Stat Screen")]
    [SerializeField] private GameObject playerStatScreen;
    [SerializeField] private TextMeshProUGUI LVL, HP, MP, EXP, NEXT, STR, INT, DEF, LCK, GOLD;
    //
    private PlayerStats playerStats;
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            playerStats = player.GetComponent<PlayerStats>();
        }
        playerHUD.SetActive(true);
        if (isTesting) { testDisplay.SetActive(true); }
    }
    private void Update()
    {
        if (testDisplay.activeInHierarchy) 
        {
            dirX = Input.GetAxisRaw("Horizontal");
            dirY = Input.GetAxisRaw("Vertical");
            textDirX.text = "X input: " + dirX.ToString();
            textDirY.text = "Y input: " + dirY.ToString();
        }

        if (playerHUD.activeInHierarchy)
        {
            textHealthHUD.text = "HP " + playerStats.currentHP.ToString() + "/" 
            + playerStats.maxHP.ToString();
            textManaHUD.text = "MP " + playerStats.currentMP.ToString() + "/"
            + playerStats.maxMP.ToString();
        }

        if (playerStatScreen.activeInHierarchy) // toggle stat screen and player hud
        {
            if (Input.GetButtonDown("StatScreen")) {
            ExitStatScreen();
            }
        }
        else {
            if (Input.GetButtonDown("StatScreen")) {
            EnterStatScreen();
            }
        }
    }

    private void EnterStatScreen()
    {
        PauseGame();
        playerHUD.SetActive(false);
        testDisplay.SetActive(false);
        playerStatScreen.SetActive(true);
        UpdateStatScreen();

    }
    private void ExitStatScreen()
    {
        UnPauseGame();
        playerStatScreen.SetActive(false);
        playerHUD.SetActive(true);
        if (isTesting) { testDisplay.SetActive(true); }
    }
    private void UpdateStatScreen()
    {
        LVL.text = "LEVEL: " + playerStats.GetCurrentLevel(playerStats.EXP, playerStats.levelReq).ToString();
        HP.text = "HP: " + playerStats.currentHP.ToString() + "/" 
        + playerStats.maxHP.ToString();
        MP.text = "MP: " + playerStats.currentMP.ToString() + "/"
        + playerStats.maxMP.ToString();
        EXP.text = "EXP: " + playerStats.EXP.ToString();
        NEXT.text = "NEXT: " + playerStats.GetExpForNextLevel(playerStats.EXP, 
        playerStats.levelReq).ToString();
        STR.text = "STR: " + playerStats.STR.ToString();
        INT.text = "INT: " + playerStats.INT.ToString();
        DEF.text = "DEF: " + playerStats.DEF.ToString();
        LCK.text = "LCK: " + playerStats.LCK.ToString();
        GOLD.text = "GOLD: " + playerStats.GOLD.ToString();
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
