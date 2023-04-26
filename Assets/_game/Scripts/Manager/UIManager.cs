using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Joystick:")]
    [SerializeField] private Joystick joystick;
    [Header("Alive:")]
    [SerializeField] private Text aliveText;
    [SerializeField] private GameObject aliveTextObj;
    [Header("Panels:")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    public Text loseText;
    [Header("Coins:")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject coin;
    [Header("Shop:")]
    [SerializeField] private GameObject weaponShop;
    [SerializeField] private GameObject hatArea;
    [SerializeField] private GameObject pantsArea;
    [SerializeField] private GameObject shieldArea;
    [SerializeField] private GameObject fullsetArea;
    [Header("Names and Indicators:")]
    [SerializeField] private GameObject indicators;
    [SerializeField] private GameObject CanvasName;
    [Header("Settings")]
    [SerializeField] private GameObject settingsObj;
    [SerializeField] private GameObject settingsPanel;
    [Header("Input Field:")]
    [SerializeField] private InputField inputField;
    [Header("PlayerName:")]
    [SerializeField] private TextMeshProUGUI playerName;

    //singleton
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideJoystick();
        HideCanvasName();
        HideIndicators();
        HideAliveText();
    }

    private void Update()
    {
        aliveText.text = LevelManager.instance.currentAlive.ToString();
    }

    //close all
    public void CloseAll()
    {
        HideJoystick();
        HideLosePanel();
        HideWinPanel();
        HideWeaponShop();
        HideIndicators();
        HideCanvasName();
        HideAliveText();
        HideCoin();
        HideSettingsObj();
        HideSettingsPanel();
    }
    
    //joystick
    public void ShowJoystick()
    {
        CloseAll();
        joystick.gameObject.SetActive(true);
        joystick.enabled = true;
    }

    public void HideJoystick()
    {
        joystick.enabled = false;
        joystick.gameObject.SetActive(false);
    }

    //lose panel
    public void ShowLosePanel()
    {
        CloseAll();
        ShowCanvasName();
        losePanel.SetActive(true);
    }

    public void HideLosePanel()
    {
        losePanel.SetActive(false);
    }

    //win panel
    public void ShowWinPanel()
    {
        CloseAll();
        winPanel.SetActive(true);
    }

    public void HideWinPanel()
    {
        winPanel.SetActive(false);
    }

    //coin
    public void UpdateUICoin()
    {
        coinText.text = DataManager.ins.playerData.coin.ToString();
    }

    //weapon shop
    public void ShowWeaponShop()
    {
        weaponShop.SetActive(true);
    }

    public void HideWeaponShop()
    {
        weaponShop.SetActive(false);
    }

    //indicators
    public void ShowIndicators()
    {
        indicators.SetActive(true);
    }

    public void HideIndicators()
    {
        indicators.SetActive(false);
    }

    //canvas name
    public void ShowCanvasName()
    {
        CanvasName.SetActive(true);
    }

    public void HideCanvasName()
    {
        CanvasName.SetActive(false);
    }

    //alive text
    public void ShowAliveText()
    {
        aliveTextObj.SetActive(true);
    }

    public void HideAliveText()
    {
        aliveTextObj.SetActive(false);
    }

    public void ShowCoin()
    {
        coin.SetActive(true);
    }

    public void HideCoin()
    {
        coin.SetActive(false);
    }

    public void ShowSettingsObj()
    {
        settingsObj.SetActive(true);
    }

    public void HideSettingsObj()
    {
        settingsObj.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void SetPlayerNameFromInputField()
    {
        playerName.text = inputField.text;
        SavePlayerName();
    }

    public void SetPlayerNameAndInputField(string str)
    {
        playerName.text = str;
        inputField.text = str;
        SavePlayerName();
    }

    public void SavePlayerName()
    {
        DataManager.ins.playerData.playerNameString = playerName.text;
    }

    //choose areas
    public void HideAllItemChooseAreas()
    {
        hatArea.SetActive(false);
        pantsArea.SetActive(false);
        shieldArea.SetActive(false);
        fullsetArea.SetActive(false);
    }
}