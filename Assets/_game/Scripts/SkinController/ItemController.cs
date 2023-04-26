using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemController : MonoBehaviour
{
    [Header("Player:")]
    public PlayerWearSkinItems player;
    [Header("Button Array:")]
    public Text buyButtonText;
    public Button[] buttons;
    [Header("Items Array:")]
    public Item[] items;
    [Header("Outline:")]
    public Outline[] outlines;
    [Header("Indexs:")]
    public int currentIndex = 0;
    public int usingIndex = -1;
    [Header("Item Type:")]
    public ItemType itemType;


    protected virtual void Start()
    {
        AddEventToAllItems();
        items[Constant.FIRST_INDEX].DisplayOutline(); 
    }

    protected virtual void UnDisplayAllOutlines()
    {
        for(int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            item.UnDisplayOutline();
        }
    }

    public virtual void OnButtonClick(int index)
    {
        Item item = items[index];
        if (item.isPurchased == false)
        {
            UpdateBuyButton(item.cost.ToString());
        }
        else
        {
            if (usingIndex == index)
            {
                UpdateBuyButton(Constant.USING);
            }
            else
            {
                UpdateBuyButton(Constant.USE);
            }
        }
        currentIndex = index;
        SkinShopManager.instance.currentItem = item;

        UnDisplayAllOutlines();
        item.DisplayOutline();
    }

    public void UpdateBuyButton(string str)
    {
        buyButtonText.text = str;
    }

    public void AddEventToAllItems()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int localIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(localIndex));
        }
    }

    public void LoadIsPurchasedData() 
    {
        //duyet dict de tim tim ra itemcontroller, sau do set tat ca isPurchased = data.isPurchased
        for(int i = 0; i < DataManager.ins.playerData.dict.Length; i++)
        {
            if (DataManager.ins.playerData.dict[i].itemType == this.itemType)
            {
                for(int j=0;j< DataManager.ins.playerData.dict[i].isPurchaseds.Length; j++)
                {
                    items[j].isPurchased = DataManager.ins.playerData.dict[i].isPurchaseds[j];
                    if (items[j].isPurchased == true)
                    {
                        OnButtonClick(j);
                        SkinShopManager.instance.UnlockSkin(items[j]);
                    }
                }
            }
        }
        //mua usingitem
        for (int i = 0; i < DataManager.ins.playerData.usingItemIndexs.Length; i++)
        {
            if (DataManager.ins.playerData.usingItemIndexs[i] >= 0 && i == (int)this.itemType)
            {
                OnButtonClick(DataManager.ins.playerData.usingItemIndexs[i]);
                SkinShopManager.instance.BuyItem(DataManager.ins.playerData.dict[i].itemType);
            }
        }
    }

}
