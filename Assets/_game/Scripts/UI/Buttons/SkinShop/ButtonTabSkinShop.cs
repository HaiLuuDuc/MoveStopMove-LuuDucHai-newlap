using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTabSkinShop : BaseButton
{
    [SerializeField] private GameObject area;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private PlayerWearSkinItems player;
    [SerializeField] private ItemController itemController;
    public Image backGround;
    public ItemType itemType;

    protected override void OnClick()
    {
        SkinShopManager.instance.currentItemType = itemType;
        SkinShopManager.instance.CloseAllBuyButtons();
        SkinShopManager.instance.ShowAllTabButtonsBackground();
        UIManager.instance.HideAllItemChooseAreas();
        ShowArea();
        ShowBuyButton();
        player.DestroyAllItemsOnBody();
        PutItemOnPlayerBody();
        HideBackground();
        itemController.OnButtonClick(itemController.currentIndex);
    }

    public void ShowArea()
    {
        area.SetActive(true);
    }

    public void ShowBuyButton()
    {
        buyButton.SetActive(true);
    }

    public void PutItemOnPlayerBody()
    {
        int index = SkinShopManager.instance.itemControllers[(int)itemType].currentIndex;
        switch (itemType)
        {
            case ItemType.Hat:
                player.WearHat(index);
                break;
            case ItemType.Pants:
                player.WearPants(index);
                break;
            case ItemType.Shield:
                player.WearShield(index);
                break;
            case ItemType.FullSet:
                player.WearFullSet(index);
                break;
        }
    }

    public void ShowBackground()
    {
        backGround.gameObject.SetActive(true);
    }

    public void HideBackground()
    {
        backGround.gameObject.SetActive(false);
    }
}
