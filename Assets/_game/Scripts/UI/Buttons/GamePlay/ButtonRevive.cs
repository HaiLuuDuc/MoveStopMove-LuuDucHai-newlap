using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRevive : BaseButton
{
    [SerializeField] private int reviveCost;
    [SerializeField] private Player player;


    protected override void OnClick()
    {
        if(DataManager.ins.playerData.coin >= reviveCost)
        {
            DataManager.ins.playerData.coin -= reviveCost;
            UIManager.instance.HideRevivePanel();
            UIManager.instance.ShowJoystick();
            BotManager.instance.EnableAllBots();
            player.OnRevive();
        }
    }
}
