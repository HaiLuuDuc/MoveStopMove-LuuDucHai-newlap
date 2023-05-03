using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkin : BaseButton
{
    [SerializeField] private Player player;

    protected override void OnClick()
    {
        CameraController.instance.StartCoroutine(CameraController.instance.SwitchTo(CameraState.Skin));
        SkinShopManager.instance.OnOpenSkinShop();
        player.Dance();
    }
}
