using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonContinue : BaseButton
{
    protected override void OnClick()
    {
        UIManager.instance.HideSettingsPanel();
        UIManager.instance.ShowJoystick();
        UIManager.instance.ShowSettingsObj();
        LevelManager.instance.isPause = false;
    }
}
