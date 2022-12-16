using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeaponUI : UICanvas
{
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private TextMeshProUGUI text_gold;

    private int currentIndex;
    private int currentItem;
    public override void Open()
    {
        base.Open();
        text_gold.text = GameManager.Ins.userData.Gold + "";
        currentItem = GameManager.Ins.userData.lastUsedWeapon;
        currentIndex = currentItem;
        if (currentItem != 0)
        {
            weapons[0].GetComponent<EquipWeaponText>().SetUnEquipText(); ;
            weapons[currentItem].GetComponent<EquipWeaponText>().SetEquipText();
        }
        OpenCurrentPanel();
    }
    public override void Close()
    {
        base.Close();
        CloseCurrentPanel();
    }
    public void ChangeCurrentItem(int i)
    {
        if (currentItem != i)
        {
            if (GameManager.Ins.soundMode)
            {
                AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
            }
            weapons[currentItem].GetComponent<EquipWeaponText>().SetUnEquipText(); ;
            currentItem = i;
            weapons[currentItem].GetComponent<EquipWeaponText>().SetEquipText();
            LevelManager.Ins.player.ChangeWeapon((WeaponType)currentItem);
        }
    }
    public void EquipButton()
    {
        ChangeCurrentItem(currentIndex);
    }
   
    public void NextItemButton()
    {
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        CloseCurrentPanel();
        currentIndex++;
        if (currentIndex > weapons.Count - 1)
        {
            currentIndex = 0;
        }
        if(currentIndex < 0)
        {
            currentIndex = weapons.Count - 1;
        }
        OpenCurrentPanel();
    }

    public void BackItemButton()
    {
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        CloseCurrentPanel();
        currentIndex--;
        if (currentIndex > weapons.Count - 1)
        {
            currentIndex = 0;
        }
        if (currentIndex < 0)
        {
            currentIndex = weapons.Count - 1;
        }
        OpenCurrentPanel();
    }

    public void X_Button()
    {
        if (GameManager.Ins.soundMode)
        {
            AudioManager.Ins.Play(Constant.SOUND_BUTTONCLICK);
        }
        UIManager.Ins.OpenUI<MainMenu>();
        Close();
    }

    public void OpenCurrentPanel()
    {
        weapons[currentIndex].SetActive(true);
    }

    public void CloseCurrentPanel()
    {
        weapons[currentIndex].SetActive(false);
    }
}
