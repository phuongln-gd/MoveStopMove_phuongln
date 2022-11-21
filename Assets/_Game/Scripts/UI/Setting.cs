using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void Close()
    {
        Time.timeScale = 1;
        base.Close();
    }

    public void HomeButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close();
    }

    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        Close();
    }
}
