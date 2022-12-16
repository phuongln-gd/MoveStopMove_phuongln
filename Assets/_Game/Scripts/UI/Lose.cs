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
        LevelManager.Ins.IsGameOver = true;
        int coin_Rand = Random.Range(10, 15);
        int coin_player = coin_Rand + GameManager.Ins.userData.Gold;
        GameManager.Ins.userData.SetIntData(UserData.Key_Gold, ref GameManager.Ins.userData.Gold, coin_player);
        SetText(coin_Rand + "");
        SetTextLose();
        LevelManager.Ins.targetIndicatorManager.SetEnable(false);
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_LOSE);
        }
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

    public void WatchVideoButton()
    {
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
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
