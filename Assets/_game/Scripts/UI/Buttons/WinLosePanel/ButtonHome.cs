using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHome : BaseButton
{
    protected override void OnClick()
    {
        CameraController.instance.StartCoroutine(CameraController.instance.SwitchTo(CameraState.MainMenu));
        LevelManager.instance.DeleteCharacters();
        LevelManager.instance.RespawnCharacters();
        LevelManager.instance.ResetTargetCircle();
        LevelManager.instance.isGaming = false;
        LevelManager.instance.isPause = false;
        LevelManager.instance.isWin = false;
        LevelManager.instance.SpawnMap(LevelManager.instance.currentLevelIndex);
        LevelManager.instance.SpawnNav(LevelManager.instance.currentLevelIndex);
        BotManager.instance.DisableAllBots();
        UIManager.instance.ShowCoin();
        UIManager.instance.ShowSound();
        UIManager.instance.HideSettingsObj();
        UIManager.instance.HideJoystick();
    }
}
