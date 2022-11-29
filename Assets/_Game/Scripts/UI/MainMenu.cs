using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    [SerializeField] private List<Image> phoneStates;
    [SerializeField] private List<Image> soundStates;
    [SerializeField] private TextMeshProUGUI text_gold;

    public override void Open()
    {
        base.Open();
        text_gold.text = GameManager.Ins.userData.Gold+"";
        LevelManager.Ins.player.SetEnableCanvasName(false);
        GameManager.Ins.ChangeCameraMainMenu();
    }
    public void PlayButton()
    {
        LevelManager.Ins.OnStartGame();
        UIManager.Ins.OpenUI<GamePlay>();
        Close();
    }

    public void ChangeWeaponButton()
    {
        UIManager.Ins.OpenUI<ChangeWeaponUI>();
        Close();
    }

    public void ChangeSkinButton()
    {
        UIManager.Ins.OpenUI<ChangeSkinUI>();
        Close();
    }

    public void SoundButton()
    {
        if (soundStates[0].gameObject.activeSelf)
        {
            soundStates[0].gameObject.SetActive(false);
            soundStates[1].gameObject.SetActive(true);
        }
        else
        {
            soundStates[0].gameObject.SetActive(true);
            soundStates[1].gameObject.SetActive(false);
        }
    }

    public void PhoneButton()
    {
        if (phoneStates[0].gameObject.activeSelf)
        {
            phoneStates[0].gameObject.SetActive(false);
            phoneStates[1].gameObject.SetActive(true);
        }
        else
        {
            phoneStates[0].gameObject.SetActive(true);
            phoneStates[1].gameObject.SetActive(false);
        }
    }

}
