using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.UI;

[Serializable]
public struct IsPurchasedItems
{
    public ItemType itemType;
    public bool[] isPurchaseds;
}
public class DataManager : MonoBehaviour
{
    public static DataManager ins;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        this.LoadData();
    }
    public PlayerWearSkinItems player;
    public bool isLoaded = false;
    public PlayerData playerData;
    public const string PLAYER_DATA = "PLAYER_DATA";


    private void OnApplicationPause(bool pause) { SaveData(); }
    private void OnApplicationQuit() { SaveData(); }


    public void LoadData()
    {
        string d = PlayerPrefs.GetString(PLAYER_DATA, "");
        if (d != "")
        {
            playerData = JsonUtility.FromJson<PlayerData>(d);
        }
        else
        {
            playerData = new PlayerData();
        }

       //load
        LoadIsPurchasedItems();
        LoadItemsOnPlayerBody();
        LoadPlayerNameAndInputField();

             // sau khi hoàn thành tất cả các bước load data ở trên
             isLoaded = true;
        // FirebaseManager.Ins.OnSetUserProperty();  
    }

    public void SaveData()
    {
        if (!isLoaded) return;
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYER_DATA, json);
    }

    public void LoadIsPurchasedItems()
    {
        for(int i = 0; i < SkinShopManager.instance.itemControllers.Length; i++)
        {
            SkinShopManager.instance.itemControllers[i].LoadIsPurchasedData();
        }
    }

    public void LoadItemsOnPlayerBody()
    {
        player.PutOnItems();
    }

    public void LoadPlayerNameAndInputField()
    {
        UIManager.instance.SetPlayerNameAndInputField(playerData.playerNameString);
    }

   
}


[System.Serializable]
public class PlayerData
{
    /*[Header("--------- Game Setting ---------")]
    public bool isNew = true;
    public bool isMusic = true;
    public bool isSound = true;
    public bool isVibrate = true;
    public bool isNoAds = false;
    public int starRate = -1;*/


    [Header("--------- Game Params ---------")]
    public bool isSetUp = false;
    public int level = 0;
    public int coin = 900;
    public int[] usingItemIndexs = new int[10];
    public IsPurchasedItems[] dict = new IsPurchasedItems[4];
    public Material currentBodyMat;
    public string playerNameString;

    public PlayerData()
    {
        if(isSetUp == true)
        {
            goto Label;
        }
        for(int i=0;i<usingItemIndexs.Length;i++)
        {
            usingItemIndexs[i] = -1;
        }
        SetUpDict();
        isSetUp = true;
    Label:;
    }
    public void SetUpDict()
    {
        dict[0].itemType = ItemType.Hat;
        dict[0].isPurchaseds = new bool[9];
        dict[1].itemType = ItemType.Pants;
        dict[1].isPurchaseds = new bool[9];
        dict[2].itemType = ItemType.Shield;
        dict[2].isPurchaseds = new bool[2];
        dict[3].itemType = ItemType.FullSet;
        dict[3].isPurchaseds = new bool[3];
    }
}

