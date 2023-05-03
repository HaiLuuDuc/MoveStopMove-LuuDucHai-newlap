using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum ItemType
{
    Hat = 0, Pants = 1, Shield = 2, FullSet = 3
}
public class SkinShopManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerWearSkinItems player;
    [Header("Items")]
    public GameObject[] hats;
    public Material[] pants;
    public GameObject[] shields;
    public GameObject[] fullSet;
    [Header("Item Controllers")]
    public ItemController[] itemControllers;
    public ItemType currentItemType;
    public Item currentItem;
    [Header("Buttons")]
    public ButtonBuySkinShop[] buyButtons;
    public ButtonTabSkinShop[] tabButtons;

    //singleton
    public static SkinShopManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void OnOpenSkinShop()
    {
        //khi mở shop thì luôn hiển thị tab hat
        tabButtons[(int)ItemType.Hat].RunOnClick();
    }

    public void OnCloseSkinShop()
    {
        // đóng shop thì player lên đồ
        player.PutOnItems();
    }

    public void BuyItem(ItemType type)
    {
        ItemController ic = itemControllers[(int)type];
        Item item = ic.items[ic.currentIndex];
        //nếu chưa mua và đủ tiền mua
        if (item.isPurchased == false && DataManager.ins.playerData.coin >= item.cost)
        {
            item.isPurchased = true;// đánh dấu là đã mua
            DataManager.ins.playerData.coin -= item.cost;//trừ tiền của người chơi
            UIManager.instance.UpdateUICoin();//update coin hiển thị
            ic.UpdateBuyButton(Constant.USING);//button thay đổi từ số tiền sang USING
            ic.usingIndex = ic.currentIndex;//đánh dấu item này đang được sử dụng
            UnlockSkin(item);//tắt UI khóa
        }
        //nếu đã mua 
        else if (item.isPurchased == true)
        {
            //nếu đang không sử dụng item này thì USE -> USING
            if (ic.usingIndex != ic.currentIndex)
            {
                ic.UpdateBuyButton(Constant.USING);
                ic.usingIndex = ic.currentIndex;
            }
            //nếu đang sử dụng item này thì USING -> USE
            else
            {
                ic.UpdateBuyButton(Constant.USE);
                ic.usingIndex = -1;
            }
            UnlockSkin(item);
        }

        //đánh dấu item này isPurchased = true trong Data
        if (ic.usingIndex >= 0) {
            SaveIsPurchasedItem(ic.itemType, ic.usingIndex);
        }



    }

    public void CloseAllBuyButtons()
    {
        for (int i = 0; i < buyButtons.Length; i++)
        {
            buyButtons[i].gameObject.SetActive(false);
        }
    }

    public void ShowAllTabButtonsBackground()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            tabButtons[i].ShowBackground();
        }
    }

    public void UnlockSkin(Item skin)
    {
        skin.lockObj.SetActive(false);
    }

    public void SaveIsPurchasedItem(ItemType type, int index)
    {
        for (int i = 0; i < DataManager.ins.playerData.dict.Length; i++)
        {
            if (DataManager.ins.playerData.dict[i].itemType == type)
            {
                DataManager.ins.playerData.dict[i].isPurchaseds[index] = true;
            }
        }
    }

    public void SaveUsingItemIndex(ItemType type)
    {
        ItemController ic = null;
        //get itemController
        for (int i = 0; i < itemControllers.Length; i++)
        {
            if (itemControllers[i].itemType == type)
            {
                ic = itemControllers[i];
            }
        }
        if (ic != null)
        {
            DataManager.ins.playerData.usingItemIndexs[(int)ic.itemType] = ic.usingIndex;
        }
    }

    public void SetOtherUsingIndexToMinusOne(Item item, ItemType type)
    {
        if (item.isPurchased)
        {
            //tat ca cac item con lai usingindex = -1
            for (int i = 0; i < itemControllers.Length; i++)
            {
                if (itemControllers[i].itemType != item.itemType)
                {
                    itemControllers[i].currentIndex = 0;
                    itemControllers[i].usingIndex = -1;
                    DataManager.ins.playerData.usingItemIndexs[i] = -1;
                }
            }
        }
    }
}


