using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lose : UICanvas
{
    [SerializeField] private TextMeshProUGUI txt;
    public override void Open()
    {
        base.Open();
        SetTextLose();
    }

    public void SetTextLose()
    {
        txt.text = $"You killed by {LevelManager.Ins.player.namekiller}\n #{LevelManager.Ins.player.rank} ";
    }
    public void HomeButton()
    {
        SimplePool.CollectAll();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
        Close();
    }

    public void WatchVideoButton()
    {

    }

}
