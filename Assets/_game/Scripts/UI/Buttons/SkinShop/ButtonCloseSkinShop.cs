using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCloseSkinShop : BaseButton
{
    [SerializeField] private Player player;
    protected override void OnClick()
    {
        CameraController.instance.StartCoroutine(CameraController.instance.SwitchTo(CameraState.MainMenu));
        SkinShopManager.instance.OnCloseSkinShop();
        player.Idle();
    }
}
