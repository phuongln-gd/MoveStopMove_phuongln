using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lose : UICanvas
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private TextMeshProUGUI text_coin;

    public override void Open()
    {
        base.Open();
        int coin_Rand = Random.Range(10, 15);
        SetText(coin_Rand + "");
        SetTextLose();
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
    public void SetTextLose()
    {
        txt.text = $"You killed by {LevelManager.Ins.player.namekiller}\n #{LevelManager.Ins.player.rank} ";
    }

    public void SetText(string s)
    {
        text_coin.text = s;
    }
}
