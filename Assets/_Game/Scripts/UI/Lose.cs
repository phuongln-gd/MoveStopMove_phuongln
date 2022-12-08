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
        int coin_player = coin_Rand + GameManager.Ins.userData.Gold;
        GameManager.Ins.userData.SetIntData(UserData.Key_Gold, ref GameManager.Ins.userData.Gold, coin_player);
        SetText(coin_Rand + "");
        SetTextLose();
        AudioManager.Ins.Play(Constant.SOUND_LOSE);
    }

    public void HomeButton()
    {
        SimplePool.CollectAll();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
        AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        Close();
    }

    public void WatchVideoButton()
    {
        AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
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
