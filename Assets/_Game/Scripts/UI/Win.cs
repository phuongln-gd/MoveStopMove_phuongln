using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Win : UICanvas
{
    [SerializeField] private TextMeshProUGUI text_coin;
    [SerializeField] private TextMeshProUGUI text_killed;

    public override void Open()
    {
        base.Open();
        int coin_Rand = Random.Range(30, 40);
        Debug.Log(coin_Rand);
        SetText(coin_Rand+"");
        SetTextKilled(LevelManager.Ins.player.killCount + "");
    }
    public void HomeButton()
    {
        SimplePool.CollectAll();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
        Close();
    }

    public void SetText(string s)
    {
        text_coin.text = s;
    }

    public void SetTextKilled(string s)
    {
        text_killed.text = s;
    }
}
