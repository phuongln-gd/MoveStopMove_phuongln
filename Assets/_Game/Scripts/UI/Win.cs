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
        LevelManager.Ins.IsGameOver = true;
        int coin_Rand = Random.Range(30, 40);
        int coin_player = coin_Rand + GameManager.Ins.userData.Gold;
        GameManager.Ins.userData.SetIntData(UserData.Key_Gold, ref GameManager.Ins.userData.Gold, coin_player);
        SetText(coin_Rand+"");
        SetTextKilled(LevelManager.Ins.player.killCount + "");
        LevelManager.Ins.levelIndex += 1;
        GameManager.Ins.userData.SetIntData(UserData.Key_PlayingLevel, ref GameManager.Ins.userData.PlayingLevel, LevelManager.Ins.levelIndex);
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_WIN);
        }
        LevelManager.Ins.targetIndicatorManager.SetEnable(false);
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

    public void NextButton()
    {
        LevelManager.Ins.LoadLevel(LevelManager.Ins.levelIndex);
        GameManager.Ins.ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<GamePlay>();
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
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
