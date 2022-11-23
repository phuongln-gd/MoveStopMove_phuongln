using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI alive_text;

    public override void Open()
    {
        int aliveBot = LevelManager.Ins.currentLevel.aliveBot;
        UIManager.Ins.GetUI<GamePlay>().SetAliveText("Alive: " + aliveBot);
    }
    public void Settingbutton()
    {
        GameManager.Ins.ChangeState(GameState.Setting);
        UIManager.Ins.OpenUI<Setting>();
        Close();
    }

    public void SetAliveText(string text)
    {
        alive_text.text = text;
    }
}
