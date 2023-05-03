using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSettings : BaseButton
{
    protected override void OnClick()
    {
        UIManager.instance.ShowSettingsPanel();
        UIManager.instance.HideJoystick();
        UIManager.instance.HideSettingsObj();
        LevelManager.instance.isPause = true;
    }
}
