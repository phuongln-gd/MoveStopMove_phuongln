using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI alive_text;

    public override void Open()
    {
        base.Open();
        int characterAmount = LevelManager.Ins.currentLevel.aliveBot + 1;
        UIManager.Ins.GetUI<GamePlay>().SetAliveText("Alive: " + characterAmount);
        LevelManager.Ins.player.SetEnableCanvasName(true);
        GameManager.Ins.ChangeCameraGamePlay();
        LevelManager.Ins.targetIndicatorManager.SetEnable(true);
    }
    public void Settingbutton()
    {
        GameManager.Ins.ChangeState(GameState.Setting);
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        UIManager.Ins.OpenUI<Setting>();
        Close();
    }

    public void SetAliveText(string text)
    {
        alive_text.text = text;
    }
   
}
