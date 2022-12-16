using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    [SerializeField] private List<Image> soundStates;
    [SerializeField] private TextMeshProUGUI text_gold;

    public override void Open()
    {
        base.Open();
        text_gold.text = GameManager.Ins.userData.Gold+"";
        LevelManager.Ins.player.SetEnableCanvasName(false);
        GameManager.Ins.ChangeCameraMainMenu();
        LevelManager.Ins.targetIndicatorManager.SetEnable(false);
        if (!GameManager.Ins.soundMode)
        {
            SetMusicOff();
        }
    }
    public void PlayButton()
    {
        LevelManager.Ins.OnStartGame();
        UIManager.Ins.OpenUI<GamePlay>();
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        Close();
    }

    public void ChangeWeaponButton()
    {
        UIManager.Ins.OpenUI<ChangeWeaponUI>();
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        Close();
    }

    public void ChangeSkinButton()
    {
        UIManager.Ins.OpenUI<ChangeSkinUI>();
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        Close();
    }

    public void SoundButton()
    {
        if (soundStates[0].gameObject.activeSelf)
        {
            SetMusicOn();
        }
        else
        {
            SetMusicOff();
        }
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
    }

    public void SetMusicOn()
    {
        soundStates[0].gameObject.SetActive(false);
        soundStates[1].gameObject.SetActive(true);
        GameManager.Ins.soundMode = true;
        GameManager.Ins.userData.SetBoolData(UserData.Key_MusicIsOn, ref GameManager.Ins.userData.musicIsOn, true);
    }

    public void SetMusicOff()
    {
        soundStates[0].gameObject.SetActive(true);
        soundStates[1].gameObject.SetActive(false);
        GameManager.Ins.soundMode = false;
        GameManager.Ins.userData.SetBoolData(UserData.Key_MusicIsOn, ref GameManager.Ins.userData.musicIsOn, false);
    }
}
