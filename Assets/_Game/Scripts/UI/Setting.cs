using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
        LevelManager.Ins.targetIndicatorManager.SetEnable(false);
    }

    public override void Close()
    {
        Time.timeScale = 1;
        base.Close();
    }

    public void HomeButton()
    {
        SimplePool.CollectAll();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        Close();
    }

    public void ContinueButton()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay);
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        UIManager.Ins.OpenUI<GamePlay>();
        Close();
    }
}
